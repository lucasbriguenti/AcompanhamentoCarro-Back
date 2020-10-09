using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Services.Validators;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly FuncionarioValidator validator;
        private readonly FuncionarioCargoValidator validatorFuncCargo;
        private readonly NotificationContext _notification;

        public FuncionarioController(IMapper mapper, IUnitOfWork uow, NotificationContext notification)
        {
            _mapper = mapper;
            _uow = uow;
            _notification = notification;
            validator = new FuncionarioValidator();
            validatorFuncCargo = new FuncionarioCargoValidator();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuncionarioDto dto)
        {
            var funcionarioRegistrado = _uow.FuncionarioRepositorio.Get(x => x.Cpf.Equals(dto.Cpf.LimpaMascaraCnpjCpf()));
            if (funcionarioRegistrado != null)
            {
                _notification.AddNotification("0", $"Funcionário {dto.Cpf} já cadastrado");
                return NotFound();
            }
            var funcionario = _mapper.Map<Funcionario>(dto);
            funcionario.Validate(funcionario, validator);
            if (funcionario.Invalid)
            {
                _notification.AddNotifications(funcionario.ValidationResult);
                return NotFound();
            }
            _uow.FuncionarioRepositorio.Adicionar(funcionario);
            await _uow.Commit();
            return Ok(funcionario);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var funcionarios = await _uow.FuncionarioRepositorio.GetTudoAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _uow.FuncionarioRepositorio.GetAsync(x => x.Id == id));
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FuncionarioFiltro filtro)
        {
            if (filtro.IsNull)
                return await Get();

            return Ok(await _uow.FuncionarioRepositorio.GetTudoAsync(x =>
            (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome)) &&
            (string.IsNullOrEmpty(filtro.Cpf) || x.Cpf.Equals(filtro.Cpf.LimpaMascaraCnpjCpf())) &&
            (!filtro.DataInicioContratacao.HasValue || x.DataContratacao >= filtro.DataInicioContratacao.Value) &&
            (!filtro.DataFimContratacao.HasValue || x.DataContratacao <= filtro.DataFimContratacao.Value)));
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto dto)
        {
            var funcionario = await _uow.FuncionarioRepositorio.GetAsync(x => x.Id == id);
            if (funcionario == null)
            {
                _notification.AddNotification("0", "Funcionário não cadastrado");
                return NotFound();
            }
            funcionario = _mapper.Map<Funcionario>(dto);
            funcionario.Validate(funcionario, validator);

            if (funcionario.Invalid)
            {
                _notification.AddNotifications(funcionario);
                return NotFound();
            }

            _uow.FuncionarioRepositorio.Atualizar(funcionario);
            await _uow.Commit();
            return Ok(funcionario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var funcionario = await _uow.FuncionarioRepositorio.GetAsync(x => x.Id == id);
            if (funcionario == null)
            {
                _notification.AddNotification("0", "Funcionário não cadastrado");
                return NotFound();
            }

            _uow.FuncionarioRepositorio.Deletar(funcionario);
            await _uow.Commit();
            return Ok(funcionario);
        }

        /// <summary>
        /// Vincula um funcionario a uma empresa
        /// </summary>
        /// <param name="idFuncionario">Id do funcionario</param>
        /// <param name="idEmpresa">Id da empresa</param>
        /// <returns></returns>
        [HttpPut("vincular/{idFuncionario}/empresa/{idEmpresa}")]
        public async Task<IActionResult> Vincular(int idFuncionario, int idEmpresa)
        {
            var funcionario = _uow.FuncionarioRepositorio.Get(x => x.Id == idFuncionario);
            var empresa = _uow.EmpresaRepositorio.Get(x => x.Id == idEmpresa);
            if (empresa != null && funcionario != null && funcionario.Empresa == null)
            {
                funcionario.Empresa = empresa;
                _uow.FuncionarioRepositorio.Atualizar(funcionario);
                await _uow.Commit();
                return Ok(funcionario);
            }
            else
            {
                _notification.AddNotification("Vinculação", "Erro na vinculação a empresa");
                return NotFound();
            }
                
        }
        [HttpPut("vincularcargo")]
        public async Task<IActionResult> VincularCargo([FromQuery] VincularFuncionarioCargoDto dto)
        {
            var funcionario = _uow.FuncionarioRepositorio.Get(x => x.Id == dto.FuncionarioId);
            var cargo = _uow.CargoRepositorio.Get(x => x.Id == dto.CargoId);
            if (cargo != null && funcionario != null && !funcionario.FuncionarioCargos.Any(x => x.CargoId == cargo.Id) && funcionario.Empresa != null)
            {
                var funcionarioCargo = _mapper.Map<FuncionarioCargo>(dto);
                funcionarioCargo.Validate(funcionarioCargo, validatorFuncCargo);
                if(funcionarioCargo.Invalid)
                {
                    _notification.AddNotifications(funcionarioCargo);
                    return NotFound();
                }

                _uow.FuncionarioCargoRepositorio.Adicionar(funcionarioCargo);
                await _uow.Commit();
                return Ok(funcionarioCargo);
            }
            else
            {
                _notification.AddNotification("0", "Impossivel vincular cargo");
                return NotFound();
            }
        }
    }
}
