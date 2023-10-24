using SandubaApi.Context;
using SandubaApi.Models;
using SandubaApi.Repository.Interfaces;

namespace SandubaApi.Repository.Implementations
{
    public class IngredienteRepository : Repository<Ingrediente>, IIngredienteRepository
    {
        public IngredienteRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
