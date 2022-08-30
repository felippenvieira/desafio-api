using System;

namespace desafio_api.DTO
{
    public class ClienteDTO
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
    }
}