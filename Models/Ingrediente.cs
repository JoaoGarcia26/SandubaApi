using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SandubaApi.Models;
public class Ingrediente
{
    [Key]
    public int IngredienteId { get; set; }
    [Required(ErrorMessage = "O nome do ingrediente é obrigatório")]
    [MaxLength(40, ErrorMessage = "O nome do ingrediente deve conter até 40 caracteres.")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "A quantidade do ingrediente é obrigatória")]
    public int Quantidade { get; set; }
    public int CategoriaId { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }
    public Ingrediente() { }
    public Ingrediente(int Id, string Nome, int Quantidade, Categoria Categoria)
    {
        IngredienteId = Id;
        this.Nome = Nome;
        this.Quantidade = Quantidade;
        this.Categoria = Categoria;
        this.CategoriaId = Categoria.CategoriaId;
    }
}
