using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Models.Classes;
using Microsoft.AspNetCore.Authorization;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FuncionarioController : BaseController<Funcionario>
    {
        public FuncionarioController(IService<Funcionario> service) : base(service)
        {
            
        }


        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FuncionarioFiltro filtro)
        {
            if (filtro.IsNull)
                return await Get();

            return Ok(await _service.GetTudo(x =>
            (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome)) &&
            (string.IsNullOrEmpty(filtro.Cpf) || x.Cpf.Equals(filtro.Cpf.LimpaMascaraCnpjCpf()))));
        }
    }
}
