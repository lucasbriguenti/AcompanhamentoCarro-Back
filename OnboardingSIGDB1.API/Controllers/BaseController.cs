using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Models.Classes;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase where T : Entity
    {
        protected readonly IService<T> _service;

        public BaseController(IService<T> service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T entity)
        {
            if (_service.Armazenar(entity))
            {
                await _service.Commit();
                return Ok(entity);
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _service.GetTudo();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.Get(x => x.Id == id));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] T entity)
        {
            if (_service.Armazenar(entity, id))
            {
                await _service.Commit();
                return Ok(entity);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_service.Excluir(id))
            {
                await _service.Commit();
                return Ok(await _service.Get(x => x.Id == id));
            }
            return NotFound();
        }
    }
}
