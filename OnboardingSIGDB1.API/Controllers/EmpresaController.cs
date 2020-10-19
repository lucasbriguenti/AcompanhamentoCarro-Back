using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Models.Classes;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IService<Empresa> _service;
        private readonly IMapper _mapper;
        public EmpresaController(IService<Empresa> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaDto dto)
        {
            var empresa = _mapper.Map<Empresa>(dto);
            if(_service.Armazenar(empresa))
            {
                await _service.Commit();
                return Ok(empresa);
            }
            return NotFound();
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> GetAsync([FromQuery] EmpresaFiltro filtro)
        {
            if (filtro.IsNull)
                return await Get();

            return Ok(await _service.GetTudo(x =>
            (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome)) &&
            (string.IsNullOrEmpty(filtro.Cnpj) || x.Cnpj.Equals(filtro.Cnpj.LimpaMascaraCnpjCpf())) &&
            (!filtro.DataFimFundacao.HasValue || x.DataFundacao.HasValue && x.DataFundacao <= filtro.DataFimFundacao) &&
            (!filtro.DataInicioFundacao.HasValue || x.DataFundacao.HasValue && x.DataFundacao >= filtro.DataInicioFundacao)));
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
        public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
        {
            var empresa = _mapper.Map<Empresa>(dto);
            if (_service.Armazenar(empresa, id))
            {
                await _service.Commit();
                return Ok(empresa);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_service.Excluir(id))
            {
                await _service.Commit();
                return Ok();
            }
            return NotFound();
        }
    }
}
