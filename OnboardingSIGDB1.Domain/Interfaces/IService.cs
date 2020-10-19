using OnboardingSIGDB1.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Interfaces
{
    public interface IService<T> where T : Entity
    {
        bool Armazenar(T obj, int? id = null);
        bool Excluir(int id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetTudo(Expression<Func<T, bool>> predicate = null);
        Task<int> Commit();
    }
}
