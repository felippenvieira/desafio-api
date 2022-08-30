using System;

namespace desafio_api.DTO
{
    public class AtendimentoDTO
    {
        public Guid AtendimentoID { get; set; }
        public Guid MedicoId { get; set; }
        public Guid CachorroId { get; set; }
        public DateTime Data { get; set; }
        public string Diagnostico { get; set; }
        public string Comentarios { get; set; }
    }
}