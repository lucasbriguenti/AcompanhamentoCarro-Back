using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardingSIGDB1.API.Migrations
{
    public partial class atualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CargoId1",
                table: "FuncionarioCargos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FuncionarioCargos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCargos_CargoId1",
                table: "FuncionarioCargos",
                column: "CargoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioCargos_Empresas_CargoId1",
                table: "FuncionarioCargos",
                column: "CargoId1",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioCargos_Empresas_CargoId1",
                table: "FuncionarioCargos");

            migrationBuilder.DropIndex(
                name: "IX_FuncionarioCargos_CargoId1",
                table: "FuncionarioCargos");

            migrationBuilder.DropColumn(
                name: "CargoId1",
                table: "FuncionarioCargos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FuncionarioCargos");
        }
    }
}
