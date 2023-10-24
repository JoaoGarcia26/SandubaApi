using SandubaApi.Models;

namespace SandubaApi.Repository.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetCategoriasIngredientesAsync();
    }
}
