using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services.Validators;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly CargoValidator validator;
        private readonly NotificationContext _notification;

        public CargoController(IMapper mapper, IUnitOfWork uow, NotificationContext notification)
        {
            _mapper = mapper;
            _uow = uow;
            _notification = notification;
            validator = new CargoValidator();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoDto dto)
        {
            var cargo = _mapper.Map<Cargo>(dto);
            cargo.Validate(cargo, validator);
            if(cargo.Invalid)
            {
                _notification.AddNotifications(cargo);
                return NotFound();
            }

            _uow.CargoRepositorio.Adicionar(cargo);
            await _uow.Commit();
            return Ok(cargo);

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _uow.CargoRepositorio.GetTudoAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _uow.CargoRepositorio.GetAsync(x => x.Id == id));
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CargoDto dto)
        {
            var cargo = await _uow.CargoRepositorio.GetAsync(x => x.Id == id);
            if (cargo == null || id != dto.Id)
            {
                _notification.AddNotification("0", "Cargo não cadastrado");
                return NotFound();
            }
            cargo = _mapper.Map<Cargo>(dto);
            cargo.Validate(cargo, validator);
            if (cargo.Invalid)
            {
                _notification.AddNotifications(cargo);
                return NotFound();
            }
            
            _uow.CargoRepositorio.Atualizar(cargo);
            await _uow.Commit();
            return Ok(cargo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _uow.CargoRepositorio.GetAsync(x => x.Id == id);
            if (cargo == null)
            {
                _notification.AddNotification("0", "Cargo não encontrado");
                return NotFound();
            }
                
            _uow.CargoRepositorio.Deletar(cargo);
            await _uow.Commit();
            return Ok(cargo);
        }
    }
}
