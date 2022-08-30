using desafio_api.Models;
using FluentAssertions;
using Xunit;

namespace desafio_tests
{
    public class AtendimentosTest
    {
        private Atendimento _atendimento;

        [Theory]
        [InlineData("tá ruim o bichinho")]
        public void AtualizarDiagnostico(string diagnostico)
        {
            PreencheAtendimento();
            _atendimento.AtualizarDiagnostico(diagnostico);
            _atendimento.Diagnostico.Should().Be(diagnostico);
        }

        [Theory]
        [InlineData("cachorrinho gente boa")]
        public void CriarComentario(string comentarios)
        {
            PreencheAtendimento();
            var comentarioAntigo = _atendimento.Comentarios;
            _atendimento.AtualizarDiagnostico(comentarios);
            var comentarioNovo = comentarioAntigo + "\r\n" + comentarios;
            _atendimento.Diagnostico.Should().Be(comentarios);
        }

        public void PreencheAtendimento()
        {
            var diagnostico = "não tem nada mesmo";
            var comentarios = "cachorro acima do peso";
            _atendimento = new Atendimento(diagnostico, comentarios);
        }
    }
}
