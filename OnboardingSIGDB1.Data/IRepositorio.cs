using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data
{

    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> GetTudo(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetTudoAsync(Expression<Func<T, bool>> predicate = null);
        T Get(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null);
        void Adicionar(T entity);
        void AdicionarAsync(T entity);
        void Atualizar(T entity);
        void Deletar(T entity);
        int Contar();
    }
}
