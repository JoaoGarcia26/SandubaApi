using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using SandubaApi.Context;
using SandubaApi.Models;
using SandubaApi.Repository.Interfaces;

namespace SandubaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public IngredientesController(IUnitOfWork context) 
        {
            _uow = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ingrediente>> Get()
        {
            try
            {
                var lista = _uow.IngredienteRepository.Get().ToList();
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
        public ActionResult<Ingrediente> GetPorId(int id)
        {
            try
            {
                var item = _uow.IngredienteRepository.GetById(i => i.IngredienteId == id);
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
        public ActionResult Post(Ingrediente ingrediente)
        {
            try
            {
                if (ingrediente is null)
                {
                    return BadRequest("Ingrediente não pode conter valores nulos.");
                }
                _uow.IngredienteRepository.Update(ingrediente);
                _uow.Commit();
                _logger.Info(" Metodo POST Ingredientes executado com sucesso! ");
                return new CreatedAtRouteResult("ObterIngrediente", new { id = ingrediente.IngredienteId }, ingrediente);
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo POST Ingredientes. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Ingrediente ingrediente)
        {
            try
            {
                if (id != ingrediente.IngredienteId)
                {
                    return BadRequest("Ingrediente não encontrado");
                }
                _uow.IngredienteRepository.Update(ingrediente);
                _uow.Commit();
                _logger.Info(" Metodo PUT Ingredientes executado com sucesso! ");
                return Ok();
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo PUT Ingredientes. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Ingrediente> Delete(int id)
        {
            try
            {
                var item = _uow.IngredienteRepository.GetById(i => id == i.IngredienteId);
                if (item is null)
                {
                    return NotFound("Ingrediente não encontrado");
                }
                _uow.IngredienteRepository.Delete(item);
                _uow.Commit();
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
