using System;
using System.Collections.Generic;

namespace desafio_api.Models
{
    public class Cachorro
    {
        public Guid CachorroId { get; init; }
        public string Nome { get; private set; }
        public string Raca { get; private set; }
        public ICollection<DadosCachorro> DadosCachorro { get; private set; } = new List<DadosCachorro>();
        public Cliente Cliente { get; private set; }
        public ICollection<Atendimento> Atendimentos { get; private set; } = new List<Atendimento>();

        public Cachorro(string nome, string raca)
        {
            CachorroId = Guid.NewGuid();
            Nome = nome;
            Raca = raca;
        }

        public void AdicionarAtendimento(Atendimento atendimento)
        {
            Atendimentos.Add(atendimento);
        }

        public void AdicionarDados(DadosCachorro dados)
        {
            DadosCachorro.Add(dados);
        }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarRaca(string raca)
        {
            Raca = raca;
        }
    }
}