using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSB.Portal.Backend.Infra.Migrations
{
    public partial class listadesinistros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coberturas_Sinistros_SinistroId",
                table: "Coberturas");

            migrationBuilder.DropIndex(
                name: "IX_Coberturas_SinistroId",
                table: "Coberturas");

            migrationBuilder.DropColumn(
                name: "SinistroId",
                table: "Coberturas");

            migrationBuilder.AddColumn<long>(
                name: "CoberturaId",
                table: "Sinistros",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sinistros_CoberturaId",
                table: "Sinistros",
                column: "CoberturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sinistros_Coberturas_CoberturaId",
                table: "Sinistros",
                column: "CoberturaId",
                principalTable: "Coberturas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sinistros_Coberturas_CoberturaId",
                table: "Sinistros");

            migrationBuilder.DropIndex(
                name: "IX_Sinistros_CoberturaId",
                table: "Sinistros");

            migrationBuilder.DropColumn(
                name: "CoberturaId",
                table: "Sinistros");

            migrationBuilder.AddColumn<long>(
                name: "SinistroId",
                table: "Coberturas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coberturas_SinistroId",
                table: "Coberturas",
                column: "SinistroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coberturas_Sinistros_SinistroId",
                table: "Coberturas",
                column: "SinistroId",
                principalTable: "Sinistros",
                principalColumn: "Id");
        }
    }
}
