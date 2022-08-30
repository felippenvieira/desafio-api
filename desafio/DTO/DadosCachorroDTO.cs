using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_api.DTO
{
    public class DadosCachorroDTO
    {
        public Guid DadosCachorroId { get; set; }
        public double Peso { get; set; }
        public double Largura { get; set; }
        public double Altura { get; set; }
        public Guid CachorroId { get; set; }
        public DateTime Data { get; set; }
    }
}