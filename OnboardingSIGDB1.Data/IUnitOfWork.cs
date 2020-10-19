using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data
{
    public interface IUnitOfWork<T> where T : class 
    {
        IRepositorio<T> Repositorio { get; }
        Task<int> Commit();
    }
}
