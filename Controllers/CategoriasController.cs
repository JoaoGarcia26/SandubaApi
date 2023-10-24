using Microsoft.AspNetCore.Mvc;
using NLog;
using SandubaApi.Models;
using SandubaApi.Repository.Interfaces;

namespace SandubaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CategoriasController(IUnitOfWork context)
        {
            _uow = context;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasIngredientesAsync()
        {
            try
            {
                var categorias = await _uow.CategoriaRepository.GetCategoriasIngredientesAsync();
                _logger.Info(" Metodo GET Categorias executado com sucesso! ");
                if (categorias is null)
                {
                    return NotFound("Categorias não encontradas");
                }
                return categorias.ToList();
            } catch
            {
                _logger.Info(" Erro ao executar o metodo GET Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var listaCategorias = _uow.CategoriaRepository.Get().ToList();
                _logger.Info(" Metodo GET Categorias executado com sucesso! ");
                if (listaCategorias is null)
                {
                    return NotFound("Categorias não encontradas");
                }
                return listaCategorias;
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo GET Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }

        }
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetPorId(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
                _logger.Info($" Metodo GET por ID ({id}) Categorias executado com sucesso! ");
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }
                return categoria;
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo GET Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest("Categoria não pode ser nula");
                }
                _uow.CategoriaRepository.Add(categoria);
                _uow.Commit();
                _logger.Info(" Metodo POST Categorias executado com sucesso! ");
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo POST Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return NotFound("Categoria não pode ser nula");
                }
                else if (id != categoria.CategoriaId)
                {
                    return BadRequest("Categoria não existe");
                }
                _uow.CategoriaRepository.Update(categoria);
                _uow.Commit();
                _logger.Info(" Metodo PUT Categorias executado com sucesso! ");
                return Ok();
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo PUT Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
                if (categoria is null)
                {
                    return NotFound("Categoria não existe");
                }
                _uow.CategoriaRepository.Delete(categoria);
                _uow.Commit();
                _logger.Info(" Metodo DELETE Categorias executado com sucesso! ");
                return categoria;
            }
            catch
            {
                _logger.Info(" Erro ao executar o metodo DELETE Categorias. ");
                throw new Exception("Ocorreu um erro interno em nosso sistema.");
            }
        }
    }
}
