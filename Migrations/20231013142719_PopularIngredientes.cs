using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SandubaApi.Migrations
{
    public partial class PopularIngredientes : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Pão sem glúten', 10, 1)");
            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Pão de hambúrguer', 10, 1)");

            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Frango grelhado', 10, 2)");
            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Bacon', 10, 2)");

            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Queijo cheddar', 10, 3)");
            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Queijo mussarela', 10, 3)");

            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Alface', 10, 4)");
            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Tomate', 10, 4)");

            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Maionese', 10, 5)");
            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Ketchup', 10, 5)");

            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Cebola caramelizada', 10, 6)");
            mb.Sql("INSERT INTO Ingredientes(Nome, Quantidade, CategoriaId) VALUES ('Guacamole', 10, 6)");
        }
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Ingredientes");
        }
    }
}
