using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using desafio_api.Data;
using desafio_api.Models;
using desafio_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using desafio_api.Services;

namespace desafio_api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITheDogService _theDogService;

        public AtendimentosController(AppDbContext context, ITheDogService theDogService)
        {
            _context = context;
            _theDogService = theDogService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAtendimentos()
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var atendimentos = await _context.Atendimentos
                .AsNoTracking()
                .Include(x => x.Cachorro)
                .Include(x => x.Medico)
                .ToListAsync();

            if (atendimentos is null)
                return BadRequest(new { Message = "Desculpe, mas nenhum atendimento foi encontrado." });

            var lista = new List<AtendimentoDTO>();

            atendimentos.ForEach(x => lista
                .Add(new AtendimentoDTO
                {
                    AtendimentoID = x.AtendimentoId,
                    MedicoId = x.Medico.MedicoId,
                    CachorroId = x.Cachorro.CachorroId,
                    Diagnostico = x.Diagnostico,
                    Comentarios = x.Comentarios
                }));

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAtendimentoById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var atendimento = await _context.Atendimentos
                .AsNoTracking()
                .Include(x => x.Cachorro)
                .Include(x => x.Medico)
                .FirstOrDefaultAsync(x => x.AtendimentoId == id);

            if (atendimento is null)
                return BadRequest(new { Message = "Desculpe, mas nenhum atendimento foi encontrado." });

            var atendimentoDto = new AtendimentoDTO
            {
                AtendimentoID = atendimento.AtendimentoId,
                MedicoId = atendimento.Medico.MedicoId,
                CachorroId = atendimento.Cachorro.CachorroId,
                Diagnostico = atendimento.Diagnostico,
                Comentarios = atendimento.Comentarios
            };

            return Ok(atendimentoDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAtendimento(AtendimentoDTO atendimentoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var atendimento = new Atendimento(atendimentoDTO.Diagnostico, atendimentoDTO.Comentarios);


            var medico = await _context.Medicos.FirstOrDefaultAsync(x => x.MedicoId == atendimentoDTO.MedicoId);
            if (medico is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum médico com esse id." });
            medico.AdicionarAtendimento(atendimento);

            var cachorro = await _context
                .Cachorros
                .Include(x => x.DadosCachorro)
                .FirstOrDefaultAsync(x => x.CachorroId == atendimentoDTO.CachorroId);
            if (cachorro is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cachorro com esse id." });
            cachorro.AdicionarAtendimento(atendimento);

            var dadosCachorro = await _context.DadosCachorros.FirstOrDefaultAsync(x => x.Cachorro == cachorro);
            var dadosDaRaca = await _theDogService.GetBreedByName(cachorro.Raca);

            if (dadosDaRaca is not null)
            {
                foreach (var item in dadosDaRaca)
                {
                    var comentario = $"Peso atual: {dadosCachorro.Peso}\r\nPeso ideal: {item.weight.metric}\r\nAltura atual: {dadosCachorro.Altura}\r\nAltura ideal: {item.height.metric}\r\n";
                    atendimento.CriarComentario(comentario);
                }
            }


            await _context.Atendimentos.AddAsync(atendimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAtendimentoById), new { id = atendimento.AtendimentoId }, atendimentoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarAtendimento(AtendimentoDTO atendimentoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var atendimento = await _context.Atendimentos
                .Include(x => x.Cachorro)
                .Include(x => x.Medico)
                .FirstOrDefaultAsync(x => x.AtendimentoId == atendimentoDTO.AtendimentoID);

            if (atendimentoDTO.CachorroId != atendimento.Cachorro.CachorroId)
            {
                var cachorro = await _context.Cachorros.FirstOrDefaultAsync(x => x.CachorroId == atendimento.Cachorro.CachorroId);
                if (cachorro is null)
                    return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cachorro com esse id." });
                cachorro.AdicionarAtendimento(atendimento);
            }

            if (atendimentoDTO.MedicoId != atendimento.Medico.MedicoId)
            {
                var medico = await _context.Medicos.FirstOrDefaultAsync(x => x.MedicoId == atendimento.Medico.MedicoId);
                if (medico is null)
                    return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum médico com esse id." });
                medico.AdicionarAtendimento(atendimento);
            }

            atendimento.AtualizarDiagnostico(atendimentoDTO.Diagnostico);
            atendimento.AtualizarComentarios(atendimentoDTO.Comentarios);

            await _context.SaveChangesAsync();

            return Ok(atendimentoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarAtendimento(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var atendimento = await _context.Atendimentos.FirstOrDefaultAsync(x => x.AtendimentoId == id);
            if (atendimento is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum atendimento com esse id." });

            _context.Atendimentos.Remove(atendimento);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}