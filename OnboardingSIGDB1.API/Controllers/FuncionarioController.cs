using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFuncionarioService _service;

        public FuncionarioController(IMapper mapper, IFuncionarioService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuncionarioDto dto)
        {
            var funcionario = _mapper.Map<Funcionario>(dto);
            if(_service.Armazenar(funcionario))
            {
                await _service.Commit();
                return Ok(funcionario);
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

        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FuncionarioFiltro filtro)
        {
            if (filtro.IsNull)
                return await Get();

            return Ok(await _service.GetTudo(x =>
            (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome)) &&
            (string.IsNullOrEmpty(filtro.Cpf) || x.Cpf.Equals(filtro.Cpf.LimpaMascaraCnpjCpf())) &&
            (!filtro.DataInicioContratacao.HasValue || x.DataContratacao >= filtro.DataInicioContratacao.Value) &&
            (!filtro.DataFimContratacao.HasValue || x.DataContratacao <= filtro.DataFimContratacao.Value)));
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto dto)
        {
            var funcionario = _mapper.Map<Funcionario>(dto);
            if (_service.Armazenar(funcionario, id))
            {
                await _service.Commit();
                return Ok(funcionario);
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

        /// <summary>
        /// Vincula um funcionario a uma empresa
        /// </summary>
        /// <param name="idFuncionario">Id do funcionario</param>
        /// <param name="idEmpresa">Id da empresa</param>
        /// <returns></returns>
        [HttpPut("vincular/{idFuncionario}/empresa/{idEmpresa}")]
        public async Task<IActionResult> VincularEmpresa(int idFuncionario, int idEmpresa)
        {
            if(_service.VincularEmpresa(idFuncionario, idEmpresa))
            {
                await _service.Commit();
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("vincularcargo")]
        public async Task<IActionResult> VincularCargo([FromQuery] VincularFuncionarioCargoDto dto)
        {
            if (_service.VincularCargo(dto))
            {
                await _service.Commit();
                return Ok();
            }
            return NotFound();
        }
    }
}
