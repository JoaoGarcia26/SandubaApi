using Microsoft.AspNetCore.Mvc;
using SandubaApi.Context;
using SandubaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SandubaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

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
                if (categorias is null)
                {
                    return NotFound("Categorias não encontradas");
                }
                return categorias;
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
        {
            try
            {
                var listaCategorias = await _context.Categorias.AsNoTracking().ToListAsync();
                if (listaCategorias is null)
                {
                    return NotFound("Categorias não encontradas");
                }
                return listaCategorias;
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
            
        }
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetPorIdAsync(int id)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }
                return categoria;
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Categoria não pode ser nula");
            }
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("ObterCategoria", new { id =  categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, Categoria categoria)
        {
            if (categoria is null)
            {
                return NotFound("Categoria não pode ser nula");
            } else if (id != categoria.CategoriaId)
            {
                return BadRequest("Categoria não existe");
            }
            _context.Categorias.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Categoria>> DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não existe");
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}
