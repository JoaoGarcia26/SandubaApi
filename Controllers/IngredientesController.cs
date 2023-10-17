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
            var lista = _context.Ingredientes.ToList();
            if (lista is null)
            {
                return NotFound("Nenhum ingrediente cadastrado");
            }
            return Ok(lista);
        }

        [HttpGet("{id:int}", Name = "ObterIngrediente")]
        public ActionResult GetPorId(int id)
        {
            var item = _context.Ingredientes.FirstOrDefault(i => i.IngredienteId == id);
            if (item is null)
            {
                return NotFound("Ingrediente não encontrado");
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post(Ingrediente ingrediente)
        {
            if (ingrediente is null)
            {
                return BadRequest("Ingrediente não pode conter valores nulos.");
            }
            _context.Ingredientes.Add(ingrediente);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterIngrediente", new { id = ingrediente.IngredienteId }, ingrediente);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Ingrediente ingrediente)
        {
            if (id != ingrediente.IngredienteId)
            {
                return BadRequest("Ingrediente não encontrado");
            }
            _context.Ingredientes.Entry(ingrediente).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(ingrediente);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            var item = _context.Ingredientes.FirstOrDefault(i => id == i.IngredienteId);
            if (item is null)
            {
                return NotFound("Ingrediente não encontrado");
            }
            _context.Ingredientes.Remove(item);
            _context.SaveChanges();
            return Ok(item);
        }
    }
}
