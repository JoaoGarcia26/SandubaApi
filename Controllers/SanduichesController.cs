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
            var listaSanduiches = _context.Sanduiches.ToList();
            if (listaSanduiches is null)
            {
                return NotFound("Sanduiches não encontrados");
            }
            return listaSanduiches;
        }
        [HttpGet("{id:int}", Name="ObterSanduiche")]
        public ActionResult<Sanduiche> GetSanduichePorId(int id)
        {
            var sanduiche = _context.Sanduiches.FirstOrDefault(s => s.SanduicheId == id);
            if (sanduiche is null)
            {
                return NotFound("Sanduiche não encontrado");
            }
            return sanduiche;
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

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Sanduiche sanduiche)
        {
            if (id != sanduiche.SanduicheId)
            {
                return BadRequest("O Sanduiche não existe");
            }
            _context.Entry(sanduiche).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(sanduiche);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var sanduiche = _context.Sanduiches.FirstOrDefault(s => s.SanduicheId == id);
            if (sanduiche is null)
            {
                return NotFound("Sanduiche não encontrado");
            }
            _context.Sanduiches.Remove(sanduiche);
            _context.SaveChanges();
            return Ok(sanduiche);
        }
    }
}
