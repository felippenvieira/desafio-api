using desafio_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_api.Data.Mappings
{
    public class MedicoMap : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medicos");

            builder.HasKey(x => x.MedicoId);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.CRMV)
                .IsRequired()
                .HasColumnName("CRMV")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

            builder.HasMany(x => x.Atendimentos)
                .WithOne(x => x.Medico);
        }
    }
}