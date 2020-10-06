using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public EmpresaController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmpresaDto dto)
        {
            var empresa = _mapper.Map<Empresa>(dto);
            try
            {
                _uow.EmpresaRepositorio.Adicionar(empresa);
                _uow.Commit();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _uow.EmpresaRepositorio.GetAsync());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_uow.EmpresaRepositorio.Get(x => x.Id == id));
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] EmpresaDto dto)
        {
            var empresa = _uow.EmpresaRepositorio.Get(x => x.Id == id);
            if (empresa == null)
                return NotFound();

            empresa = _mapper.Map<Empresa>(dto);
            _uow.EmpresaRepositorio.Atualizar(empresa);
            _uow.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var empresa = _uow.EmpresaRepositorio.Get(x => x.Id == id);
            if (empresa == null)
                return NotFound();

            _uow.EmpresaRepositorio.Deletar(empresa);
            _uow.Commit();
            return Ok();
        }
    }
}
