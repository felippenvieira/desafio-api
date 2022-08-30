using desafio_api.Models;
using FluentAssertions;
using Xunit;

namespace desafio_tests
{
    public class CachorroTest
    {
        private Cachorro _cachorro;

        [Theory]
        [InlineData("Jorginho")]
        public void AtualizarNome(string nome)
        {
            PreencheCachorro();
            _cachorro.AtualizarNome(nome);
            _cachorro.Nome.Should().Be(nome);
        }

        [Theory]
        [InlineData("pinscher")]
        public void AtualizarRaca(string raca)
        {
            PreencheCachorro();
            _cachorro.AtualizarNome(raca);
            _cachorro.Nome.Should().Be(raca);
        }

        public void PreencheCachorro()
        {
            var nome = "Robson";
            var raca = "Bulldog";
            _cachorro = new Cachorro(nome, raca);
        }
    }
}
