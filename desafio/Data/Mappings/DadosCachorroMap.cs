using desafio_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_api.Data.Mappings
{
    public class DadosCachorroMap : IEntityTypeConfiguration<DadosCachorro>
    {
        public void Configure(EntityTypeBuilder<DadosCachorro> builder)
        {
            builder.ToTable("DadosCachorros");

            builder.HasKey(x => x.DadosCachorroId);

            builder.Property(x => x.Peso)
                .IsRequired()
                .HasColumnName("Peso")
                .HasColumnType("DOUBLE");

            builder.Property(x => x.Largura)
                .IsRequired()
                .HasColumnName("Largura")
                .HasColumnType("DOUBLE");

            builder.Property(x => x.Altura)
                .IsRequired()
                .HasColumnName("Altura")
                .HasColumnType("DOUBLE");

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("Raca")
                .HasColumnType("DATETIME");
        }
    }
}