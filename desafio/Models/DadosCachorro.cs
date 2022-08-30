using System;

namespace desafio_api.Models
{
    public class DadosCachorro
    {
        public Guid DadosCachorroId { get; init; }
        public double Peso { get; private set; }
        public double Largura { get; private set; }
        public double Altura { get; private set; }
        public Cachorro Cachorro { get; private set; }
        public DateTime Data { get; private set; }

        public DadosCachorro(double peso, double largura, double altura, DateTime data)
        {
            Peso = peso;
            Largura = largura;
            Altura = altura;
            Data = data;
        }

        public void AtualizarPeso(double peso)
        {
            Peso = peso;
        }

        public void AtualizarLargura(double largura)
        {
            Largura = largura;
        }

        public void AtualizarAltura(double altura)
        {
            Altura = altura;
        }

        public void AtualizarData(DateTime data)
        {
            Data = data;
        }
    }
}