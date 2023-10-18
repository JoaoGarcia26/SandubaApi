using Microsoft.AspNetCore.Mvc;
using SandubaApi.Context;
using SandubaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SandubaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IngredientesController : ControllerBase
    {
        private AppDbContext _context;
        public IngredientesController(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }
            
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingrediente>>> GetAsync()
        {
            try
            {
                var lista = await _context.Ingredientes.AsNoTracking().ToListAsync();
                if (lista is null)
                {
                    return NotFound("Nenhum ingrediente cadastrado");
                }
                return lista;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterIngrediente")]
        public async Task<ActionResult<Ingrediente>> GetPorIdAsync(int id)
        {
            try
            {
                var item = await _context.Ingredientes.AsNoTracking().FirstOrDefaultAsync(i => i.IngredienteId == id);
                if (item is null)
                {
                    return NotFound("Ingrediente não encontrado");
                }
                return item;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Ingrediente ingrediente)
        {
            try
            {
                if (ingrediente is null)
                {
                    return BadRequest("Ingrediente não pode conter valores nulos.");
                }
                await _context.Ingredientes.AddAsync(ingrediente);
                await _context.SaveChangesAsync();
                return new CreatedAtRouteResult("ObterIngrediente", new { id = ingrediente.IngredienteId }, ingrediente);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> PutAsync(int id, Ingrediente ingrediente)
        {
            try
            {
                if (id != ingrediente.IngredienteId)
                {
                    return BadRequest("Ingrediente não encontrado");
                }
                _context.Ingredientes.Entry(ingrediente).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Ingrediente>> DeleteAsync(int id) 
        {
            var item = _context.Ingredientes.FirstOrDefault(i => id == i.IngredienteId);
            if (item is null)
            {
                return NotFound("Ingrediente não encontrado");
            }
            _context.Ingredientes.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
