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
        public ActionResult<IEnumerable<Ingrediente>> Get()
        {
            try
            {
                var lista = _context.Ingredientes.AsNoTracking().ToList();
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
        public ActionResult<Ingrediente> GetPorId(int id)
        {
            try
            {
                var item = _context.Ingredientes.AsNoTracking().FirstOrDefault(i => i.IngredienteId == id);
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
        public ActionResult Post([FromBody] Ingrediente ingrediente)
        {
            try
            {
                if (ingrediente is null)
                {
                    return BadRequest("Ingrediente não pode conter valores nulos.");
                }
                _context.Ingredientes.Add(ingrediente);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterIngrediente", new { id = ingrediente.IngredienteId }, ingrediente);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] Ingrediente ingrediente)
        {
            try
            {
                if (id != ingrediente.IngredienteId)
                {
                    return BadRequest("Ingrediente não encontrado");
                }
                _context.Ingredientes.Entry(ingrediente).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Ingrediente> Delete(int id) 
        {
            var item = _context.Ingredientes.FirstOrDefault(i => id == i.IngredienteId);
            if (item is null)
            {
                return NotFound("Ingrediente não encontrado");
            }
            _context.Ingredientes.Remove(item);
            _context.SaveChanges();
            return item;
        }
    }
}
