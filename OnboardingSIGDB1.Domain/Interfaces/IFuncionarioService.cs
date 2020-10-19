using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.Domain.Interfaces
{
    public interface IFuncionarioService : IService<Funcionario>
    {
        bool VincularEmpresa(int idFuncionario, int idEmpresa);
        bool VincularCargo(VincularFuncionarioCargoDto dto);
    }
}
