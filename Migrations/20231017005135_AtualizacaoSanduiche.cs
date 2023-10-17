using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SandubaApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoSanduiche : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SanduicheImg",
                table: "Sanduiches",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SanduicheImg",
                table: "Sanduiches");
        }
    }
}
