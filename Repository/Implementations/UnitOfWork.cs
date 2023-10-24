using Microsoft.EntityFrameworkCore;
using SandubaApi.Context;
using SandubaApi.Repository.Interfaces;

namespace SandubaApi.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private SanduicheRepository _sanduicheRepository;
        private CategoriaRepository _categoriaRepository;
        private IngredienteRepository _ingredienteRepository;
        public ISanduicheRepository SanduicheRepository 
        { 
            get 
            {
                return _sanduicheRepository = _sanduicheRepository ?? new SanduicheRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public IIngredienteRepository IngredienteRepository
        {
            get
            {
                return _ingredienteRepository = _ingredienteRepository ?? new IngredienteRepository(_context);
            }
        }
        public AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async void Commit()
        {
            await _context.SaveChangesAsync();
        }
        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
