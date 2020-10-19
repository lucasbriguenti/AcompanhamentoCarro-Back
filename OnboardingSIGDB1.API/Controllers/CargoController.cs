using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly IService<Cargo> _service;
        private readonly IMapper _mapper;

        public CargoController(IService<Cargo> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoDto dto)
        {
            var cargo = _mapper.Map<Cargo>(dto);
            if(_service.Armazenar(cargo))
            {
                await _service.Commit();
                return Ok(cargo);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetTudo());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.Get(x => x.Id == id));
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CargoDto dto)
        {
            var cargo = _mapper.Map<Cargo>(dto);
            if(_service.Armazenar(cargo, id))
            {
                await _service.Commit();
                return Ok(cargo);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(_service.Excluir(id))
            {
                await _service.Commit();
                return Ok();
            }
            return NotFound();
        }
    }
}
