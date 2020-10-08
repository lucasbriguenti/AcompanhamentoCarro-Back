using OnboardingSIGDB1.Domain.Models;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data
{
    public interface IUnitOfWork
    {
        IRepositorio<Funcionario> FuncionarioRepositorio { get; }
        IRepositorio<Empresa> EmpresaRepositorio { get; }
        IRepositorio<Cargo> CargoRepositorio { get; }
        Task<int> Commit();
    }
}
