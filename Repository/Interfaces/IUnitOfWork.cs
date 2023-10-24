namespace SandubaApi.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public ISanduicheRepository SanduicheRepository { get; }
        public ICategoriaRepository CategoriaRepository { get; }
        public IIngredienteRepository IngredienteRepository { get; }
        void Commit();
        void Dispose();
    }
}
