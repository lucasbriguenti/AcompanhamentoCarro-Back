using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardingSIGDB1.API.Migrations
{
    public partial class atualizacaoindexcargo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioCargos_Cargos_CargoId",
                table: "FuncionarioCargos");

            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioCargos_Funcionarios_FuncionarioId",
                table: "FuncionarioCargos");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioCargos_Cargos_CargoId",
                table: "FuncionarioCargos",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioCargos_Funcionarios_FuncionarioId",
                table: "FuncionarioCargos",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioCargos_Cargos_CargoId",
                table: "FuncionarioCargos");

            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioCargos_Funcionarios_FuncionarioId",
                table: "FuncionarioCargos");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioCargos_Cargos_CargoId",
                table: "FuncionarioCargos",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);


            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioCargos_Funcionarios_FuncionarioId",
                table: "FuncionarioCargos",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
