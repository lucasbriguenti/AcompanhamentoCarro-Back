using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DataContext Contexto = null;
        private readonly DbSet<T> DbSet;
        public Repositorio(DataContext context)
        {
            Contexto = context;
            DbSet = Contexto.Set<T>();
        }
        public IEnumerable<T> GetTudo(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return DbSet.Where(predicate);
            }
            return DbSet.AsEnumerable();
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetTudoAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await DbSet.Where(predicate).ToListAsync();
            }
            return await DbSet.ToListAsync();
        }
        public void Adicionar(T entity)
        {
            DbSet.Add(entity);
        }
        public void Atualizar(T entity)
        {
            DbSet.Update(entity);
        }
        public void Deletar(T entity)
        {
            DbSet.Remove(entity);
        }
        public int Contar()
        {
            return DbSet.Count();
        }


    }
}
