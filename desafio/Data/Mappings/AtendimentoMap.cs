using desafio_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_api.Data.Mappings
{
    public class AtendimentoMap : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.ToTable("Atendimentos");

            builder.HasKey(x => x.AtendimentoId);

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("DataAtendimento")
                .HasColumnType("DATETIME");

            builder.Property(x => x.Diagnostico)
                .IsRequired()
                .HasColumnName("Diagnostico")
                .HasColumnType("VARCHAR")
                .HasMaxLength(1000);

            builder.Property(x => x.Comentarios)
                .HasColumnName("Comentarios")
                .HasColumnType("VARCHAR")
                .HasMaxLength(1000);
        }
    }
}