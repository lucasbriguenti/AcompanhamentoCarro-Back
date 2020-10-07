using OnboardingSIGDB1.Domain.Models;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data
{
    public interface IUnitOfWork
    {
        IRepositorio<Empresa> EmpresaRepositorio { get; }
        Task<int> Commit();
    }
}
