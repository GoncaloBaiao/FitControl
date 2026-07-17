using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitControl.API.Migrations
{
    /// <inheritdoc />
    public partial class NewGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Altura",
                table: "Socios",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Genero",
                table: "Socios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Peso",
                table: "Socios",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Socios");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Socios");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Socios");
        }
    }
}
