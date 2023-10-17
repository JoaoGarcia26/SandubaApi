using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SandubaApi.Models;
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [MaxLength(35, ErrorMessage = "O nome da categoria pode conter até 35 caracteres.")]
    public string? Nome { get; set; }
    public ICollection<Ingrediente>? Ingredientes { get; set; }
    public Categoria() 
    {
        CategoriaId = 0;
        Nome = null;
        Ingredientes = new Collection<Ingrediente>();
    }
    public Categoria(int id, string nome)
    {
        CategoriaId = id;
        Nome = nome;
        Ingredientes = new Collection<Ingrediente>();
    }
}
