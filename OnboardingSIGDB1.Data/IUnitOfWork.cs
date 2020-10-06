using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data
{
    public interface IUnitOfWork
    {
        IRepositorio<Empresa> EmpresaRepositorio { get; }
        void Commit();
    }
}
