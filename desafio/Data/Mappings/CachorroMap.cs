using desafio_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_api.Data.Mappings
{
    public class CachorroMap : IEntityTypeConfiguration<Cachorro>
    {
        public void Configure(EntityTypeBuilder<Cachorro> builder)
        {
            builder.ToTable("Cachorros");

            builder.HasKey(x => x.CachorroId);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Raca)
                .IsRequired()
                .HasColumnName("Raca")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.HasMany(x => x.DadosCachorro)
                .WithOne(x => x.Cachorro);

            builder.HasMany(x => x.Atendimentos)
                .WithOne(x => x.Cachorro);
        }
    }
}