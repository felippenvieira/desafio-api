using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using desafio_api.Data;
using desafio_api.Models;
using desafio_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace desafio_api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetClientes()
        {
            if (!ModelState.IsValid)
                return BadRequest("Desculpe, verifique os campos e tente novamente.");

            var clientes = await _context.Clientes.AsNoTracking().ToListAsync();

            if (clientes is null)
                return BadRequest("Desculpe, mas não encontramos nenhum cliente foi encontrado.");

            var listaDeClientes = new List<ClienteDTO>();

            clientes.ForEach(x => listaDeClientes.Add(new ClienteDTO { ClienteId = x.ClienteId, Nome = x.Nome, CPF = x.CPF }));

            return Ok(listaDeClientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClienteById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Desculpe, verifique os campos e tente novamente.");

            var cliente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.ClienteId == id);

            if (cliente is null)
                return BadRequest("Desculpe, mas não encontramos nenhum cliente com o id informado.");

            var clienteDTO = new ClienteDTO
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                CPF = cliente.CPF
            };

            return Ok(clienteDTO);
        }

        [HttpGet]
        [Route("ListarAtendimentos")]
        public async Task<ActionResult> GetAtendimentos(Guid idCliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var atendimentos = await _context
                .Atendimentos
                .Include(x => x.Cachorro)
                .Include(x => x.Medico)
                .Where(x => x.Cachorro.Cliente.ClienteId == idCliente)
                .ToListAsync();

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
        public async Task<ActionResult> CreateCliente(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Desculpe, verifique os campos e tente novamente.");

            var cliente = new Cliente(clienteDTO.Nome, clienteDTO.CPF);

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.ClienteId }, clienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarCliente(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == clienteDTO.ClienteId);
            if (cliente is null)
                return BadRequest(new { Message = "Desculpe, mas não encontramos nenhum cliente com esse id." });

            cliente.AtualizarNome(clienteDTO.Nome);
            cliente.AtualizarCpf(clienteDTO.CPF);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarCliente(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == id);
            if (cliente is null)
                return BadRequest(new { Message = "Desculpe, não encontramos nenhum cliente com o id selecionado." });

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}