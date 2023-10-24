using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using SandubaApi.Models;
using SandubaApi.Repository.Interfaces;

namespace SandubaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanduichesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public SanduichesController(IUnitOfWork context)
        {
            _uow = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sanduiche>>> GetAsync()
        {
            try
            {
                var listaSanduiches = await _uow.SanduicheRepository.Get().ToListAsync();
                
                _logger.Info("Metodo GET Sanduiches executado com sucesso!");

                if (listaSanduiches is null)
                {
                    return NotFound("Sanduiches não encontrados");
                }

                return listaSanduiches;
                
            }
            catch
            {
                _logger.Info("Erro ao executar o metodo GET Sanduiches.");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
        [HttpGet("{id:int:min(1)}", Name = "ObterSanduiche")]
        public ActionResult<Sanduiche> GetSanduichePorId(int id)
        {
            try
            {
                var sanduiche = _uow.SanduicheRepository.GetById(s => s.SanduicheId == id);
                _logger.Info($" Metodo GET por ID ({id}) Sanduiches executado com sucesso! ");
                if (sanduiche is null)
                {
                    return NotFound("sanduiche não encontrado");
                }
                return sanduiche;
            }
            catch
            {
                _logger.Info($" Erro ao executar o metodo GET por ID ({id}) Sanduiches. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPost]
        public ActionResult Post(Sanduiche sanduiche)
        {
            try
            {
                if (sanduiche is null)
                {
                    return BadRequest();
                }
                _uow.SanduicheRepository.Add(sanduiche);
                _uow.Commit();
                _logger.Info(" Metodo POST Sanduiches executado com sucesso! ");
                return new CreatedAtRouteResult("ObterSanduiche", new { id = sanduiche.SanduicheId }, sanduiche);
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo POST Sanduiches. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Sanduiche sanduiche)
        {
            try
            {
                if (id != sanduiche.SanduicheId)
                {
                    return BadRequest("O Sanduiche não existe");
                }
                _uow.SanduicheRepository.Update(sanduiche);
                _uow.Commit();
                _logger.Info(" Metodo PUT Sanduiches executado com sucesso! ");
                return Ok();
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo PUT Sanduiches. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Sanduiche> Delete(int id)
        {
            try
            {
                var sanduiche = _uow.SanduicheRepository.GetById(s => s.SanduicheId == id);
                if (sanduiche is null)
                {
                    return NotFound("Sanduiche não encontrado");
                }
                _uow.SanduicheRepository.Delete(sanduiche);
                _uow.Commit();
                _logger.Info(" Metodo DELETE Sanduiches executado com sucesso! ");
                return sanduiche;
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo DELETE Sanduiches. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
    }
}
