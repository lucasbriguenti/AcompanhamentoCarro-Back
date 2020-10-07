﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Services.Validators;
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
        public EmpresaController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
            validator = new EmpresaValidator();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaDto dto)
        {
            var result = validator.Validate(dto);
            if(result.IsValid)
            {
                var empresa = _mapper.Map<Empresa>(dto);
                _uow.EmpresaRepositorio.AdicionarAsync(empresa);
                await _uow.Commit();
                return Ok();
            }
            else
            {
                return NotFound(string.Join(',', result.Errors));
            }
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _uow.EmpresaRepositorio.GetAsync());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_uow.EmpresaRepositorio.GetTudo());
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
                return NotFound();

            empresa = _mapper.Map<Empresa>(dto);
            _uow.EmpresaRepositorio.Atualizar(empresa);
            await _uow.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _uow.EmpresaRepositorio.GetAsync(x => x.Id == id);
            if (empresa == null)
                return NotFound();

            _uow.EmpresaRepositorio.Deletar(empresa);
            await _uow.Commit();
            return Ok();
        }
    }
}
