using Microsoft.EntityFrameworkCore;
using SandubaApi.Context;
using SandubaApi.Models;
using SandubaApi.Repository.Interfaces;

namespace SandubaApi.Repository.Implementations
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) : base(contexto)
        {
        }
        public async Task<IEnumerable<Categoria>> GetCategoriasIngredientesAsync()
        {
            return await Get().Include(s => s.Ingredientes).ToListAsync();
        }
    }
}
