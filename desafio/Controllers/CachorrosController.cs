using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using desafio_api.Data;
using desafio_api.Models;
using desafio_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using desafio_api.Services;
using Microsoft.AspNetCore.Authorization;

namespace desafio_api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CachorrosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITheDogService _theDogService;

        public CachorrosController(AppDbContext context, ITheDogService theDogService)
        {
            _context = context;
            _theDogService = theDogService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cachorro>>> GetCachorros()
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var cachorros = await _context.Cachorros.AsNoTracking().Include(x => x.Cliente).ToListAsync();
            if (cachorros is null)
                return BadRequest(new { Message = "Desculpe, mas nenhum cachorro foi encontrado." });

            var listaDeCachorros = new List<CachorroDTO>();

            cachorros.ForEach(x => listaDeCachorros
                .Add(new CachorroDTO
                {
                    CachorroId = x.CachorroId,
                    Nome = x.Nome,
                    Raca = x.Raca,
                    ClienteId = x.Cliente.ClienteId
                }));

            return Ok(listaDeCachorros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCachorroById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var cachorro = await _context.Cachorros
                .AsNoTracking()
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.CachorroId == id);

            if (cachorro is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cachorro com esse id." });

            var cachorroDTO = new CachorroDTO
            {
                CachorroId = cachorro.CachorroId,
                Nome = cachorro.Nome,
                Raca = cachorro.Raca,
                ClienteId = cachorro.Cliente.ClienteId
            };

            return Ok(cachorroDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCachorro(CachorroDTO cachorroDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var cachorro = new Cachorro(cachorroDTO.Nome, cachorroDTO.Raca);

            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == cachorroDTO.ClienteId);

            if (cliente is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cliente com esse id." });

            cliente.AdicionarCachorro(cachorro);

            await _context.Cachorros.AddAsync(cachorro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCachorroById), new { id = cachorro.CachorroId }, cachorroDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cachorro>> AtualizarCachorro(CachorroDTO cachorroDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var cachorro = await _context.Cachorros
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.CachorroId == cachorroDTO.CachorroId);

            if (cachorroDTO.ClienteId != cachorro.Cliente.ClienteId)
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == cachorroDTO.ClienteId);

                if (cliente is null)
                    return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cliente com esse id." });

                cliente.AdicionarCachorro(cachorro);
            }

            cachorro.AtualizarNome(cachorroDTO.Nome);
            cachorro.AtualizarRaca(cachorroDTO.Raca);

            await _context.SaveChangesAsync();

            return Ok(cachorroDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarCachorro(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var cachorro = await _context.Cachorros.FirstOrDefaultAsync(x => x.CachorroId == id);
            if (cachorro is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cachorro com o id selecionado." });

            _context.Cachorros.Remove(cachorro);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("ListarRacas")]
        public async Task<ActionResult> GetBreeds()
        {
            var breeds = await _theDogService.GetBreeds();

            return Ok(breeds);
        }

        [HttpGet("ListarRacaPorNome")]
        public async Task<ActionResult> GetBreedByName(string name)
        {
            var breedByName = await _theDogService.GetBreedByName(name);

            return Ok(breedByName);
        }
    }
}