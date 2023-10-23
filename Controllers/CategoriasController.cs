using Microsoft.AspNetCore.Mvc;
using SandubaApi.Context;
using SandubaApi.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace SandubaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CategoriasController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasIngredientesAsync()
        {
            try
            {
                var categorias = await _context.Categorias.AsNoTracking().Include(s => s.Ingredientes).ToListAsync();
                _logger.Info(" Metodo GET Categorias executado com sucesso! ");
                if (categorias is null)
                {
                    return NotFound("Categorias não encontradas");
                }
                return categorias;
            } catch
            {
                _logger.Info(" Erro ao executar o metodo GET Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
        {
            try
            {
                var listaCategorias = await _context.Categorias.AsNoTracking().ToListAsync();
                _logger.Info(" Metodo GET Categorias executado com sucesso! ");
                if (listaCategorias is null)
                {
                    return NotFound("Categorias não encontradas");
                }
                return listaCategorias;
            } catch
            {
                _logger.Info(" Erro ao executar o metodo GET Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
            
        }
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetPorIdAsync(int id)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);
                _logger.Info($" Metodo GET por ID ({id}) Categorias executado com sucesso! ");
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }
                return categoria;
            } catch
            {
                _logger.Info(" Erro ao executar o metodo GET Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest("Categoria não pode ser nula");
                }
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
                _logger.Info(" Metodo POST Categorias executado com sucesso! ");
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo POST Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return NotFound("Categoria não pode ser nula");
                }
                else if (id != categoria.CategoriaId)
                {
                    return BadRequest("Categoria não existe");
                }
                _context.Categorias.Entry(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.Info(" Metodo PUT Categorias executado com sucesso! ");
                return Ok();
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo PUT Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }  
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Categoria>> DeleteAsync(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
                if (categoria is null)
                {
                    return NotFound("Categoria não existe");
                }
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                _logger.Info(" Metodo DELETE Categorias executado com sucesso! ");
                return categoria;
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo DELETE Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
    }
}
