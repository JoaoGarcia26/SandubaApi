using Microsoft.AspNetCore.Mvc;
using SandubaApi.Models;
using Microsoft.EntityFrameworkCore;
using SandubaApi.Context;

namespace SandubaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanduichesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SanduichesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sanduiche>>> GetAsync()
        {
            try
            {
                var listaSanduiches = await _context.Sanduiches.AsNoTracking().ToListAsync();
                if (listaSanduiches is null)
                {
                    return NotFound("Sanduiches não encontrados");
                }
                return listaSanduiches;
            }
            catch
            {
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
        [HttpGet("{id:int:min(1)}", Name = "ObterSanduiche")]
        public async Task<ActionResult<Sanduiche>> GetSanduichePorIdAsync(int id)
        {
            try
            {
                var sanduiche = await _context.Sanduiches.AsNoTracking().FirstOrDefaultAsync(s => s.SanduicheId == id);
                if (sanduiche is null)
                {
                    return NotFound("sanduiche não encontrado");
                }
                return sanduiche;
            }
            catch
            {
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Sanduiche sanduiche)
        {
            try
            {
                if (sanduiche is null)
                {
                    return BadRequest();
                }
                await _context.Sanduiches.AddAsync(sanduiche);
                await _context.SaveChangesAsync();
                return new CreatedAtRouteResult("ObterSanduiche", new { id = sanduiche.SanduicheId }, sanduiche);
            } catch 
            {
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> PutAsync(int id, Sanduiche sanduiche)
        {
            try
            {
                if (id != sanduiche.SanduicheId)
                {
                    return BadRequest("O Sanduiche não existe");
                }
                _context.Entry(sanduiche).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            } catch 
            {
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Sanduiche>> DeleteAsync(int id)
        {
            try
            {
                var sanduiche = _context.Sanduiches.FirstOrDefault(s => s.SanduicheId == id);
                if (sanduiche is null)
                {
                    return NotFound("Sanduiche não encontrado");
                }
                _context.Sanduiches.Remove(sanduiche);
                await _context.SaveChangesAsync();
                return sanduiche;
            }
            catch
            {
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
    }
}
