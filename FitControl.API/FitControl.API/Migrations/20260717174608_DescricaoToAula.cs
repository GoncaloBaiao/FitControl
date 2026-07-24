using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitControl.API.Migrations
{
    /// <inheritdoc />
    public partial class DescricaoToAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Aulas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Aulas");
        }
    }
}
