using System;
using System.Collections.Generic;

namespace desafio_api.Models
{
    public class Cliente
    {
        public Guid ClienteId { get; init; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public ICollection<Cachorro> Cachorros { get; private set; } = new List<Cachorro>();

        public Cliente(string nome, string cpf)
        {
            ClienteId = Guid.NewGuid();
            Nome = nome;
            CPF = cpf;
        }

        protected Cliente() { }

        public void AdicionarCachorro(Cachorro cachorro)
        {
            Cachorros.Add(cachorro);
        }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarCpf(string cpf)
        {
            CPF = cpf;
        }
    }
}