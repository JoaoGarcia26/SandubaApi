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
        public ActionResult<IEnumerable<Categoria>> GetCategoriasIngredientes()
        {
            return _context.Categorias.AsNoTracking().Include(s => s.Ingredientes).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var listaCategorias = _context.Categorias.AsNoTracking().ToList();
            if (listaCategorias is null)
            {
                return NotFound("Categorias não encontradas");
            }
            return Ok(listaCategorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetPorId(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Categoria não pode ser nula");
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria", new { id =  categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            if (categoria is null)
            {
                return NotFound("Categoria não pode ser nula");
            } else if (id != categoria.CategoriaId)
            {
                return BadRequest("Categoria não existe");
            }
            _context.Categorias.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não existe");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
