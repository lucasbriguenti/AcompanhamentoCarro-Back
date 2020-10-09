using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                return Query().Where(predicate);
            }
            return Query().AsEnumerable();
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return Query().FirstOrDefault(predicate);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Query().FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetTudoAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await Query().Where(predicate).ToListAsync();
            }
            return await Query().ToListAsync();
        }
        public void Adicionar(T entity)
        {
            DbSet.Add(entity);
        }

        public async void AdicionarAsync(T entity)
        {
            await DbSet.AddAsync(entity);
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

        public IQueryable<T> Query(bool eager = true)
        {
            var query = Contexto.Set<T>().AsQueryable();
            if (eager)
            {
                var navigations = Contexto.Model.FindEntityType(typeof(T))
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct();

                foreach (var property in navigations)
                {
                    query = query.Include(property.Name);
                }
                    
            }
            return query;
        }
    }
}
