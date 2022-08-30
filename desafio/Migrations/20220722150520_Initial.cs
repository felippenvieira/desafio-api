using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace desafio_api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPF = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    MedicoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CRMV = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.MedicoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cachorros",
                columns: table => new
                {
                    CachorroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Raca = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClienteId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cachorros", x => x.CachorroId);
                    table.ForeignKey(
                        name: "FK_Cachorros_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    AtendimentoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MedicoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CachorroId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DataAtendimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Diagnostico = table.Column<string>(type: "VARCHAR(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comentarios = table.Column<string>(type: "VARCHAR(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.AtendimentoId);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Cachorros_CachorroId",
                        column: x => x.CachorroId,
                        principalTable: "Cachorros",
                        principalColumn: "CachorroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DadosCachorros",
                columns: table => new
                {
                    DadosCachorroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Peso = table.Column<double>(type: "DOUBLE", nullable: false),
                    Largura = table.Column<double>(type: "DOUBLE", nullable: false),
                    Altura = table.Column<double>(type: "DOUBLE", nullable: false),
                    CachorroId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Raca = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCachorros", x => x.DadosCachorroId);
                    table.ForeignKey(
                        name: "FK_DadosCachorros_Cachorros_CachorroId",
                        column: x => x.CachorroId,
                        principalTable: "Cachorros",
                        principalColumn: "CachorroId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "CPF", "Nome" },
                values: new object[,]
                {
                    { new Guid("01706624-b589-4a76-8333-0467eea7d615"), "123456789", "Robson Caetano" },
                    { new Guid("b45b4428-de85-4836-8eda-951598019322"), "789456123", "Judiscréia dos Santos" }
                });

            migrationBuilder.InsertData(
                table: "Medicos",
                columns: new[] { "MedicoId", "CRMV", "Nome" },
                values: new object[] { new Guid("da3da908-60c3-4eb2-b989-1408ae36e550"), "11254/SP", "Reinaldo Azevedo" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nome", "PasswordHash" },
                values: new object[] { new Guid("3a67c17d-7733-41f5-a148-117a37c97d24"), "admin@gft.com", "admin", "10000.d+P57Vh62pCTJhn4Cku32A==.tVCR7/JEGqsbf8hQmRJZPYjYmJfeFO0NP/y0tm0yFTc=" });

            migrationBuilder.InsertData(
                table: "Cachorros",
                columns: new[] { "CachorroId", "ClienteId", "Nome", "Raca" },
                values: new object[] { new Guid("c117bd9f-93dd-4de2-9934-3f3654edb323"), new Guid("01706624-b589-4a76-8333-0467eea7d615"), "Paçoca", "Pinscher" });

            migrationBuilder.InsertData(
                table: "Cachorros",
                columns: new[] { "CachorroId", "ClienteId", "Nome", "Raca" },
                values: new object[] { new Guid("de502ccc-c594-4bd4-a1dc-7b17251d923a"), new Guid("01706624-b589-4a76-8333-0467eea7d615"), "Ximbica", "Pinscher" });

            migrationBuilder.InsertData(
                table: "Cachorros",
                columns: new[] { "CachorroId", "ClienteId", "Nome", "Raca" },
                values: new object[] { new Guid("c4ef00d0-d3ba-42f8-b61a-6fd6c7bb7491"), new Guid("b45b4428-de85-4836-8eda-951598019322"), "Joe", "Bulldog" });

            migrationBuilder.InsertData(
                table: "Atendimentos",
                columns: new[] { "AtendimentoId", "CachorroId", "Comentarios", "DataAtendimento", "Diagnostico", "MedicoId" },
                values: new object[] { new Guid("c1bf4c30-9229-4c02-823d-01760da5c0e9"), new Guid("c117bd9f-93dd-4de2-9934-3f3654edb323"), "Cachorro ainda não estava 100%, mas o dono quis ir embora.", new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "O cachorro veio com sintomas de desidratação. Recebeu soro e foi liberado.", new Guid("da3da908-60c3-4eb2-b989-1408ae36e550") });

            migrationBuilder.InsertData(
                table: "DadosCachorros",
                columns: new[] { "DadosCachorroId", "Altura", "CachorroId", "Raca", "Largura", "Peso" },
                values: new object[,]
                {
                    { new Guid("6945ad52-c3f9-48cd-9391-44c3df1296f7"), 35.0, new Guid("c117bd9f-93dd-4de2-9934-3f3654edb323"), new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.0, 11.199999999999999 },
                    { new Guid("81f51bf9-98f7-4f3f-b67b-187149a9f7b2"), 35.0, new Guid("de502ccc-c594-4bd4-a1dc-7b17251d923a"), new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.0, 11.199999999999999 },
                    { new Guid("4d032329-09c6-4f10-b6ff-8422e5eadca1"), 35.0, new Guid("c4ef00d0-d3ba-42f8-b61a-6fd6c7bb7491"), new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.0, 11.199999999999999 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_CachorroId",
                table: "Atendimentos",
                column: "CachorroId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_MedicoId",
                table: "Atendimentos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cachorros_ClienteId",
                table: "Cachorros",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCachorros_CachorroId",
                table: "DadosCachorros",
                column: "CachorroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "DadosCachorros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Cachorros");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
