using desafio_api.Models;
using FluentAssertions;
using Xunit;

namespace desafio_tests
{
    public class MedicoTest
    {
        private Medico _medico;

        [Theory]
        [InlineData("Jorge Cruz")]
        public void AtualizarNome(string nome)
        {
            PreencheMedico();
            _medico.AtualizarNome(nome);
            _medico.Nome.Should().Be(nome);
        }

        [Theory]
        [InlineData("789456123")]
        public void AtualizarCRMV(string crmv)
        {
            PreencheMedico();
            _medico.AtualizarCRMV(crmv);
            _medico.CRMV.Should().Be(crmv);
        }

        public void PreencheMedico()
        {
            var nome = "Jodiscleison Neves";
            var crmv = "123456789";
            _medico = new Medico(nome, crmv);
        }
    }
}
