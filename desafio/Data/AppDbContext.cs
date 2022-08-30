using System;
using desafio_api.Data.Mappings;
using desafio_api.Models;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace desafio_api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Cachorro> Cachorros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DadosCachorro> DadosCachorros { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<User> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AtendimentoMap());
            modelBuilder.ApplyConfiguration(new CachorroMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new DadosCachorroMap());
            modelBuilder.ApplyConfiguration(new MedicoMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);

            var clienteUm = new
            {
                ClienteId = Guid.NewGuid(),
                Nome = "Robson Caetano",
                CPF = "123456789"
            };

            var clienteDois = new
            {
                ClienteId = Guid.NewGuid(),
                Nome = "Judiscréia dos Santos",
                CPF = "789456123"
            };

            var cachorroUm = new
            {
                CachorroId = Guid.NewGuid(),
                Nome = "Paçoca",
                Raca = "Pinscher",
                ClienteId = clienteUm.ClienteId
            };

            var cachorroDois = new
            {
                CachorroId = Guid.NewGuid(),
                Nome = "Ximbica",
                Raca = "Pinscher",
                ClienteId = clienteUm.ClienteId
            };

            var cachorroTres = new
            {
                CachorroId = Guid.NewGuid(),
                Nome = "Joe",
                Raca = "Bulldog",
                ClienteId = clienteDois.ClienteId
            };

            var dadosCachorroUm = new
            {
                DadosCachorroId = Guid.NewGuid(),
                Peso = 11.2,
                Largura = 22.0,
                Altura = 35.0,
                CachorroId = cachorroUm.CachorroId,
                Data = new DateTime(2022, 07, 15, 00, 00, 00)
            };

            var dadosCachorroDois = new
            {
                DadosCachorroId = Guid.NewGuid(),
                Peso = 11.2,
                Largura = 22.0,
                Altura = 35.0,
                CachorroId = cachorroDois.CachorroId,
                Data = new DateTime(2022, 07, 15, 00, 00, 00)
            };

            var dadosCachorroTres = new
            {
                DadosCachorroId = Guid.NewGuid(),
                Peso = 11.2,
                Largura = 22.0,
                Altura = 35.0,
                CachorroId = cachorroTres.CachorroId,
                Data = new DateTime(2022, 07, 15, 00, 00, 00)
            };

            var medicoUm = new
            {
                MedicoId = Guid.NewGuid(),
                Nome = "Reinaldo Azevedo",
                CRMV = "11254/SP"
            };

            var atendimentoUm = new
            {
                AtendimentoId = Guid.NewGuid(),
                MedicoId = medicoUm.MedicoId,
                CachorroId = cachorroUm.CachorroId,
                Data = new DateTime(2022, 07, 15, 00, 00, 00),
                Diagnostico = "O cachorro veio com sintomas de desidratação. Recebeu soro e foi liberado.",
                Comentarios = "Cachorro ainda não estava 100%, mas o dono quis ir embora."
            };

            var usuario = new
            {
                Id = Guid.NewGuid(),
                Nome = "admin",
                Email = "admin@gft.com",
                PasswordHash = PasswordHasher.Hash("admin")
            };

            modelBuilder.Entity<Cliente>()
                .HasData(
                    clienteUm,
                    clienteDois
                );

            modelBuilder.Entity<Cachorro>()
                .HasData(
                    cachorroUm,
                    cachorroDois,
                    cachorroTres
                );

            modelBuilder.Entity<DadosCachorro>()
                .HasData(
                    dadosCachorroUm,
                    dadosCachorroDois,
                    dadosCachorroTres
                );

            modelBuilder.Entity<Medico>()
                .HasData(
                    medicoUm
                );

            modelBuilder.Entity<Atendimento>()
                .HasData(
                    atendimentoUm
                );

            modelBuilder.Entity<User>()
                .HasData(
                    usuario
                );
        }
    }
}