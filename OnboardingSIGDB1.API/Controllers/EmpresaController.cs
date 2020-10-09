using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services.Validators;
using OnboardingSIGDB1.Domain.Utils;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly EmpresaValidator validator;
        private readonly NotificationContext _notification;
        public EmpresaController(IMapper mapper, IUnitOfWork uow, NotificationContext notification)
        {
            _mapper = mapper;
            _uow = uow;
            _notification = notification;
            validator = new EmpresaValidator();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaDto dto)
        {

            var empresa = _mapper.Map<Empresa>(dto);
            empresa.Validate(empresa, validator);
            if(empresa.Invalid)
            {
                _notification.AddNotifications(empresa.ValidationResult);
                return NotFound();
            }
            else
            {
                _uow.EmpresaRepositorio.AdicionarAsync(empresa);
                await _uow.Commit();
                return Ok(empresa);
            }
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> GetAsync([FromQuery] EmpresaFiltro filtro)
        {
            if (filtro.IsNull)
                return await Get();

            return Ok(await _uow.EmpresaRepositorio.GetTudoAsync(x =>
            (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome)) &&
            (string.IsNullOrEmpty(filtro.Cnpj) || x.Cnpj.Equals(filtro.Cnpj.LimpaMascaraCnpjCpf())) &&
            (!filtro.DataFimFundacao.HasValue || x.DataFundacao.HasValue && x.DataFundacao <= filtro.DataFimFundacao) &&
            (!filtro.DataInicioFundacao.HasValue || x.DataFundacao.HasValue && x.DataFundacao >= filtro.DataInicioFundacao)));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _uow.EmpresaRepositorio.GetTudoAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _uow.EmpresaRepositorio.GetAsync(x => x.Id == id));
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
        {
            var empresa = await _uow.EmpresaRepositorio.GetAsync(x => x.Id == id);
            if (empresa == null)
            {
                _notification.AddNotification("0", "Id inexistente");
                return NotFound();
            }
            empresa = _mapper.Map<Empresa>(dto);
            empresa.Validate(empresa, validator);
            if(empresa.Invalid)
            {
                _notification.AddNotifications(empresa.ValidationResult);
                return NotFound();
            }
            _uow.EmpresaRepositorio.Atualizar(empresa);
            await _uow.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _uow.EmpresaRepositorio.GetAsync(x => x.Id == id);
            if (empresa == null)
            {
                _notification.AddNotification("0", "Id inexistente");
                return NotFound();
            }

            _uow.EmpresaRepositorio.Deletar(empresa);
            await _uow.Commit();
            return Ok();
        }
    }
}
