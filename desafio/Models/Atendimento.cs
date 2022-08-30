using System;

namespace desafio_api.Models
{
    public class Atendimento
    {
        public Guid AtendimentoId { get; init; }
        public Medico Medico { get; private set; }
        public Cachorro Cachorro { get; private set; }
        public DateTime Data { get; private set; }
        public string Diagnostico { get; private set; }
        public string Comentarios { get; private set; }

        public Atendimento(string diagnostico, string comentarios)
        {
            AtendimentoId = Guid.NewGuid();
            Diagnostico = diagnostico;
            Comentarios = comentarios;
            Data = DateTime.Now;
        }

        public void AtualizarDiagnostico(string diagnostico)
        {
            Diagnostico = diagnostico;
        }

        public void CriarComentario(string comentario)
        {
            string comentarioAntigo = Comentarios;
            Comentarios = comentario + "\r\n" + comentarioAntigo;
        }

        public void AtualizarComentarios(string comentarios)
        {
            Comentarios = comentarios;
        }
    }
}