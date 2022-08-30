using System;

namespace desafio_api.DTO
{
    public class CachorroDTO
    {
        public Guid CachorroId { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public Guid ClienteId { get; set; }
    }
}