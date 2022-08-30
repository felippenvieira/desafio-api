using desafio_api.Models;
using FluentAssertions;
using Xunit;

namespace desafio_tests
{
    public class ClienteTest
    {
        private Cliente _cliente;

        [Theory]
        [InlineData("Gertrudis")]
        public void AtualizarNome(string nome)
        {
            PreencheCliente();
            _cliente.AtualizarNome(nome);
            _cliente.Nome.Should().Be(nome);
        }

        [Theory]
        [InlineData("789456123")]
        public void AtualizarRaca(string cpf)
        {
            PreencheCliente();
            _cliente.AtualizarNome(cpf);
            _cliente.Nome.Should().Be(cpf);
        }

        public void PreencheCliente()
        {
            var nome = "Gertrudes";
            var cpf = "123456789";
            _cliente = new Cliente(nome, cpf);
        }
    }
}
