using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitControl.API.Migrations
{
    /// <inheritdoc />
    public partial class TabelaGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "Socios",
                newName: "GeneroId");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Socios_GeneroId",
                table: "Socios",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Socios_Generos_GeneroId",
                table: "Socios",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Socios_Generos_GeneroId",
                table: "Socios");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropIndex(
                name: "IX_Socios_GeneroId",
                table: "Socios");

            migrationBuilder.RenameColumn(
                name: "GeneroId",
                table: "Socios",
                newName: "Genero");
        }
    }
}
