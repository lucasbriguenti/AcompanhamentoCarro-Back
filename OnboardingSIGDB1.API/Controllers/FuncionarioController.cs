using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Services.Validators;
using OnboardingSIGDB1.Domain.Utils;
namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly FuncionarioValidator validator;

        public FuncionarioController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
            validator = new FuncionarioValidator();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuncionarioDto dto)
        {
            var funcionarioRegistrado = _uow.FuncionarioRepositorio.Get(x => x.Cpf.Equals(dto.Cpf.LimpaMascaraCnpjCpf()));
            var result = validator.Validate(dto);
            if(result.IsValid && funcionarioRegistrado == null)
            {
                var funcionario = _mapper.Map<Funcionario>(dto);
                _uow.FuncionarioRepositorio.Adicionar(funcionario);
                await _uow.Commit();
                return Ok();
            }
            else
            {
                return NotFound(string.Join(',', result.Errors));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _uow.FuncionarioRepositorio.GetTudoAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _uow.FuncionarioRepositorio.GetAsync(x => x.Id == id));
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto dto)
        {
            var funcionario = await _uow.FuncionarioRepositorio.GetAsync(x => x.Id == id);
            if (funcionario == null)
                return NotFound();

            funcionario = _mapper.Map<Funcionario>(dto);
            _uow.FuncionarioRepositorio.Atualizar(funcionario);
            await _uow.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var funcionario = await _uow.FuncionarioRepositorio.GetAsync(x => x.Id == id);
            if (funcionario == null)
                return NotFound();

            _uow.FuncionarioRepositorio.Deletar(funcionario);
            await _uow.Commit();
            return Ok();
        }
    }
}
