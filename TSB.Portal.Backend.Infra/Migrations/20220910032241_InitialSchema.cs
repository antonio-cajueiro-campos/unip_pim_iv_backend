using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSB.Portal.Backend.Infra.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sinistros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorSinistro = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinistros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialId = table.Column<long>(type: "bigint", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Credentials_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credentials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Coberturas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinistroId = table.Column<long>(type: "bigint", nullable: true),
                    ValorCobertura = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coberturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coberturas_Sinistros_SinistroId",
                        column: x => x.SinistroId,
                        principalTable: "Sinistros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoricoSinistros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<long>(type: "bigint", nullable: true),
                    SinistroId = table.Column<long>(type: "bigint", nullable: true),
                    Ocorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoSinistros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoSinistros_Sinistros_SinistroId",
                        column: x => x.SinistroId,
                        principalTable: "Sinistros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricoSinistros_Users_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Apolices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoberturaId = table.Column<long>(type: "bigint", nullable: true),
                    Vigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Emissao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apolices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apolices_Coberturas_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Coberturas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoId = table.Column<long>(type: "bigint", nullable: true),
                    ApoliceId = table.Column<long>(type: "bigint", nullable: true),
                    ChavePIX = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Apolices_ApoliceId",
                        column: x => x.ApoliceId,
                        principalTable: "Apolices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clientes_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPagamentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApoliceId = table.Column<long>(type: "bigint", nullable: true),
                    IDNotaFiscal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoPagamentos_Apolices_ApoliceId",
                        column: x => x.ApoliceId,
                        principalTable: "Apolices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apolices_CoberturaId",
                table: "Apolices",
                column: "CoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ApoliceId",
                table: "Clientes",
                column: "ApoliceId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EnderecoId",
                table: "Clientes",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Coberturas_SinistroId",
                table: "Coberturas",
                column: "SinistroId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoPagamentos_ApoliceId",
                table: "HistoricoPagamentos",
                column: "ApoliceId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoSinistros_ClienteId",
                table: "HistoricoSinistros",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoSinistros_SinistroId",
                table: "HistoricoSinistros",
                column: "SinistroId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CredentialId",
                table: "Users",
                column: "CredentialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "HistoricoPagamentos");

            migrationBuilder.DropTable(
                name: "HistoricoSinistros");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Apolices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Coberturas");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Sinistros");
        }
    }
}
