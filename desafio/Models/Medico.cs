using System;
using System.Collections.Generic;

namespace desafio_api.Models
{
    public class Medico
    {
        public Guid MedicoId { get; init; }
        public string Nome { get; private set; }
        public string CRMV { get; private set; }
        public ICollection<Atendimento> Atendimentos { get; private set; } = new List<Atendimento>();

        public Medico(string nome, string crmv)
        {
            MedicoId = Guid.NewGuid();
            Nome = nome;
            CRMV = crmv;
        }

        protected Medico() { }

        public void AdicionarAtendimento(Atendimento atendimento)
        {
            Atendimentos.Add(atendimento);
        }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarCRMV(string crmv)
        {
            CRMV = crmv;
        }
    }
}