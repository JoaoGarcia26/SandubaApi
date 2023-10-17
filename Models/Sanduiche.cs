using System.ComponentModel.DataAnnotations;

namespace SandubaApi.Models;
public class Sanduiche
{
    [Key]
    public int SanduicheId { get; set; }
    [Required(ErrorMessage = "O nome do sanduiche é obrigatório")]
    [MaxLength(35, ErrorMessage = "O nome do sanduiche deve conter até 35 caracteres.")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "A descrição do sanduiche é obrigatório")]
    [MaxLength(100, ErrorMessage = "A descrição do sanduiche deve conter até 100 caracteres.")]
    public string? Descricao { get; set; }
    public string? SanduicheImg { get; set; }
    public List<Ingrediente>? Ingredientes { get; set; }
    public Sanduiche() { }
    public Sanduiche(int id, string nome, string descricao, List<Ingrediente> ingredientes, string img)
    {
        SanduicheId = id;
        Nome = nome;
        Descricao = descricao;
        Ingredientes = ingredientes;
        SanduicheImg = img;
    }
}
