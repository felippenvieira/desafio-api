using System;
using System.Threading.Tasks;
using desafio_api.Data;
using desafio_api.DTO;
using desafio_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DadosCachorrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DadosCachorrosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetDadosCachorros()
        {
            var dadosCachorros = await _context.DadosCachorros.AsNoTracking().ToListAsync();

            if (dadosCachorros is null)
                return BadRequest(new { Message = "Desculpe, mas não existe nenhum dado cadastrado." });

            return Ok(dadosCachorros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDadosCachorroById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var dadosCachorro = await _context.DadosCachorros
                .AsNoTracking()
                .Include(x => x.Cachorro)
                .FirstOrDefaultAsync(x => x.DadosCachorroId == id);

            if (dadosCachorro is null)
                return BadRequest(new { Message = "Desculpe, mas nenhum atendimento foi encontrado." });

            var dadosCachorroDTO = new DadosCachorroDTO
            {
                DadosCachorroId = dadosCachorro.DadosCachorroId,
                Peso = dadosCachorro.Peso,
                Largura = dadosCachorro.Largura,
                Altura = dadosCachorro.Altura,
                CachorroId = dadosCachorro.Cachorro.CachorroId,
                Data = dadosCachorro.Data
            };

            return Ok(dadosCachorroDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CriarNovoDadoCachorro(DadosCachorroDTO dadosCachorroDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var dadosCachorro = new DadosCachorro(
                dadosCachorroDTO.Peso,
                dadosCachorroDTO.Largura,
                dadosCachorroDTO.Altura,
                dadosCachorroDTO.Data);

            var cachorro = await _context.Cachorros.FirstOrDefaultAsync(x => x.CachorroId == dadosCachorroDTO.CachorroId);
            if (cachorro is null)
                return BadRequest(new { Message = "Desculpe, mas não encontrados nenhum cachorro com o id informado." });

            cachorro.AdicionarDados(dadosCachorro);

            await _context.DadosCachorros.AddAsync(dadosCachorro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDadosCachorroById), new { id = dadosCachorro.DadosCachorroId }, dadosCachorroDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarDadosCachorro(DadosCachorroDTO dadosCachorroDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var dadosCachorro = await _context
                .DadosCachorros
                .Include(x => x.Cachorro)
                .FirstOrDefaultAsync(x => x.DadosCachorroId == dadosCachorroDTO.DadosCachorroId);

            if (dadosCachorroDTO.CachorroId != dadosCachorro.Cachorro.CachorroId)
            {
                var cachorro = await _context.Cachorros.FirstOrDefaultAsync(x => x.CachorroId == dadosCachorro.Cachorro.CachorroId);
                if (cachorro is null)
                    return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cachorro com esse id." });
                cachorro.AdicionarDados(dadosCachorro);
            }

            dadosCachorro.AtualizarPeso(dadosCachorroDTO.Peso);
            dadosCachorro.AtualizarLargura(dadosCachorroDTO.Largura);
            dadosCachorro.AtualizarAltura(dadosCachorroDTO.Altura);
            dadosCachorro.AtualizarData(dadosCachorroDTO.Data);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarDadosCachorro(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var dadosCachorro = await _context.DadosCachorros.FirstOrDefaultAsync(x => x.DadosCachorroId == id);
            if (dadosCachorro is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum atendimento com esse id." });

            _context.DadosCachorros.Remove(dadosCachorro);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}