using System;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data
{
    public class UnitOfWork<T> : IUnitOfWork<T>, IDisposable where T : class
    {
        private readonly DataContext _contexto;
        private Repositorio<T> _repositorio = null;
        public UnitOfWork(DataContext contexto)
        {
            _contexto = contexto;
        }
        public IRepositorio<T> Repositorio
        {
            get
            {
                if(_repositorio == null)
                {
                    _repositorio = new Repositorio<T>(_contexto);
                }
                return _repositorio;
            }
        }

        public async Task<int> Commit()
        {
            return await _contexto.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
