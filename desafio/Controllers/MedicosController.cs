using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using desafio_api.Data;
using desafio_api.Models;
using desafio_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace desafio_api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class MedicosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetMedicos()
        {
            if (!ModelState.IsValid)
                return BadRequest("Desculpe, verifique os campos e tente novamente.");

            var medicos = await _context.Medicos.AsNoTracking().ToListAsync();

            if (medicos is null)
                return BadRequest("Desculpe, mas nenhum médico veterinário foi encontrado.");

            var listaDeMedicos = new List<MedicoDTO>();

            medicos.ForEach(x => listaDeMedicos
                .Add(new MedicoDTO
                {
                    MedicoId = x.MedicoId,
                    Nome = x.Nome,
                    CRMV = x.CRMV
                }));

            return Ok(listaDeMedicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMedicoById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Desculpe, verifique os campos e tente novamente.");

            var medico = await _context.Medicos.AsNoTracking().FirstOrDefaultAsync(x => x.MedicoId == id);

            if (medico is null)
                return BadRequest("Desculpe, mas não encontramos nenhum médico com esse id.");

            var medicoDTO = new MedicoDTO
            {
                MedicoId = medico.MedicoId,
                Nome = medico.Nome,
                CRMV = medico.CRMV
            };

            return Ok(medicoDTO);
        }

        [HttpGet]
        [Route("ListarAtendimentos")]
        public async Task<ActionResult> GetAtendimentos()
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var atendimentos = await _context.Atendimentos.Include(x => x.Cachorro).Include(x => x.Medico).ToListAsync();
            if (atendimentos is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum atendimento." });

            var listaDeAtendimentos = new List<AtendimentoDTO>();

            atendimentos.ForEach(x => listaDeAtendimentos.Add(new AtendimentoDTO
            {
                AtendimentoID = x.AtendimentoId,
                MedicoId = x.Medico.MedicoId,
                CachorroId = x.Cachorro.CachorroId,
                Data = x.Data,
                Diagnostico = x.Diagnostico,
                Comentarios = x.Comentarios
            }));

            return Ok(listaDeAtendimentos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMedico(MedicoDTO medicoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Desculpe, verifique os campos e tente novamente.");

            var medico = new Medico(medicoDTO.Nome, medicoDTO.CRMV);

            await _context.Medicos.AddAsync(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicoById), new { id = medico.MedicoId }, medicoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarMedico(MedicoDTO medicoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var medico = await _context.Medicos.FirstOrDefaultAsync(x => x.MedicoId == medicoDTO.MedicoId);
            if (medico is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum médico com esse id." });

            medico.AtualizarNome(medicoDTO.Nome);
            medico.AtualizarCRMV(medicoDTO.CRMV);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarMedico(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var medico = await _context.Medicos.FirstOrDefaultAsync(x => x.MedicoId == id);
            if (medico is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum médico com esse id." });

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}