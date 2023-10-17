using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SandubaApi.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias(Nome) VALUES ('Queijos')");
            mb.Sql("INSERT INTO Categorias(Nome) VALUES ('Pães')");
            mb.Sql("INSERT INTO Categorias(Nome) VALUES ('Proteínas')");
            mb.Sql("INSERT INTO Categorias(Nome) VALUES ('Legumes e Vegetais')");
            mb.Sql("INSERT INTO Categorias(Nome) VALUES ('Molhos e Condimentos')");
            mb.Sql("INSERT INTO Categorias(Nome) VALUES ('Extras')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categorias");
        }
    }
}
