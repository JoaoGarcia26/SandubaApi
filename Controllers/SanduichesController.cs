using Microsoft.AspNetCore.Mvc;
using SandubaApi.Context;
using SandubaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SandubaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SanduichesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SanduichesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Sanduiche>> Get()
        {
            try
            {
                var listaSanduiches = _context.Sanduiches.AsNoTracking().ToList();
                if (listaSanduiches is null)
                {
                    return NotFound("Sanduiches não encontrados");
                }
                return listaSanduiches;
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }
        [HttpGet("{id:int:min(1)}", Name="ObterSanduiche")]
        public ActionResult<Sanduiche> GetSanduichePorId(int id)
        {
            try
            {
                var sanduiche = _context.Sanduiches.AsNoTracking().FirstOrDefault(s => s.SanduicheId == id);
                if (sanduiche is null)
                {
                    return NotFound("Sanduiche não encontrado");
                }
                return sanduiche;
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno em nosso sistema.");
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Sanduiche sanduiche)
        {
            if (sanduiche is null)
            {
                return BadRequest();
            }
            _context.Sanduiches.Add(sanduiche);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterSanduiche", new { id = sanduiche.SanduicheId }, sanduiche);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] Sanduiche sanduiche)
        {
            if (id != sanduiche.SanduicheId)
            {
                return BadRequest("O Sanduiche não existe");
            }
            _context.Entry(sanduiche).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Sanduiche> Delete(int id)
        {
            var sanduiche = _context.Sanduiches.FirstOrDefault(s => s.SanduicheId == id);
            if (sanduiche is null)
            {
                return NotFound("Sanduiche não encontrado");
            }
            _context.Sanduiches.Remove(sanduiche);
            _context.SaveChanges();
            return sanduiche;
        }
    }
}
