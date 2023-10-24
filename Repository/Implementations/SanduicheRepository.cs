using SandubaApi.Context;
using SandubaApi.Models;
using SandubaApi.Repository.Interfaces;

namespace SandubaApi.Repository.Implementations
{
    public class SanduicheRepository : Repository<Sanduiche>, ISanduicheRepository
    {
        public SanduicheRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
