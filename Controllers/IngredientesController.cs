using Microsoft.AspNetCore.Mvc;
using SandubaApi.Context;
using SandubaApi.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace SandubaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
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
                _logger.Info(" Metodo GET Ingredientes executado com sucesso! ");
                if (lista is null)
                {
                    return NotFound("Nenhum ingrediente cadastrado");
                }
                return lista;
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo GET Ingredientes. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterIngrediente")]
        public async Task<ActionResult<Ingrediente>> GetPorIdAsync(int id)
        {
            try
            {
                var item = await _context.Ingredientes.AsNoTracking().FirstOrDefaultAsync(i => i.IngredienteId == id);
                _logger.Info($" Metodo GET por ID ({id}) Ingredientes executado com sucesso! ");
                if (item is null)
                {
                    return NotFound("Ingrediente não encontrado");
                }
                return item;
            }
            catch
            {
                _logger.Info($" Erro ao executar o metodo GET por ID ({id}) Ingredientes. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
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
                _logger.Info(" Metodo POST Ingredientes executado com sucesso! ");
                return new CreatedAtRouteResult("ObterIngrediente", new { id = ingrediente.IngredienteId }, ingrediente);
            } catch
            {
                _logger.Info(" Erro ao executar o metodo POST Ingredientes. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
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
                _logger.Info(" Metodo PUT Ingredientes executado com sucesso! ");
                return Ok();
            } catch
            {
                _logger.Info(" Erro ao executar o metodo PUT Ingredientes. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Ingrediente>> DeleteAsync(int id) 
        {
            try
            {
                var item = _context.Ingredientes.FirstOrDefault(i => id == i.IngredienteId);
                if (item is null)
                {
                    return NotFound("Ingrediente não encontrado");
                }
                _context.Ingredientes.Remove(item);
                await _context.SaveChangesAsync();
                _logger.Info(" Metodo DELETE Ingredientes executado com sucesso! ");
                return item;
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo DELETE Ingredientes. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
    }
}
