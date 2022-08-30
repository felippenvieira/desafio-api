using System;

namespace desafio_api.DTO
{
    public class MedicoDTO
    {
        public Guid MedicoId { get; set; }
        public string Nome { get; set; }
        public string CRMV { get; set; }
    }
}