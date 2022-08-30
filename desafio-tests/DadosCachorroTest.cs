using System;
using desafio_api.Models;
using FluentAssertions;
using Xunit;

namespace desafio_tests
{
    public class DadosCachorroTest
    {
        private DadosCachorro _dadosCachorro;

        [Theory]
        [InlineData(20.7)]
        public void AtualizarPeso(double peso)
        {
            PreencheDadosCachorro();
            _dadosCachorro.AtualizarPeso(peso);
            _dadosCachorro.Peso.Should().Be(peso);
        }

        [Theory]
        [InlineData(20.7)]
        public void AtualizarLargura(double largura)
        {
            PreencheDadosCachorro();
            _dadosCachorro.AtualizarLargura(largura);
            _dadosCachorro.Largura.Should().Be(largura);
        }

        [Theory]
        [InlineData(20.7)]
        public void AtualizarAltura(double altura)
        {
            PreencheDadosCachorro();
            _dadosCachorro.AtualizarAltura(altura);
            _dadosCachorro.Altura.Should().Be(altura);
        }

        public void PreencheDadosCachorro()
        {
            var peso = 15.2;
            var largura = 15.2;
            var altura = 15.2;
            var data = DateTime.Now;
            _dadosCachorro = new DadosCachorro(peso, largura, altura, data);
        }
    }
}
