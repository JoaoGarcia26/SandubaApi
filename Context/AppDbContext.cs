using Microsoft.EntityFrameworkCore;
using SandubaApi.Models;

namespace SandubaApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Sanduiche> Sanduiches { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }
}
