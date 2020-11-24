using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Interfaces;
using OnBoardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroKilometragemController : BaseController<RegistroKilometragem>
    {
        public RegistroKilometragemController(IService<RegistroKilometragem> service) : base(service)
        {

        }
    }
}
