using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardingSIGDB1.API.Migrations
{
    public partial class atualizacaoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "FuncionarioCargos");
            migrationBuilder.DropColumn(name: "CargoId1", table: "FuncionarioCargos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FuncionarioCargos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
