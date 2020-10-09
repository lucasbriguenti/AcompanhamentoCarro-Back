using OnboardingSIGDB1.Domain.Models;
using System;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _contexto;
        private Repositorio<Empresa> _empresaRepositorio = null;
        private Repositorio<Funcionario> _funcionarioRepositorio = null;
        private Repositorio<Cargo> _cargoRepositorio = null;
        private Repositorio<FuncionarioCargo> _funcionarioCargoRepositorio = null;
        public UnitOfWork(DataContext contexto)
        {
            _contexto = contexto;
        }
        public IRepositorio<Empresa> EmpresaRepositorio
        {
            get
            {
                if(_empresaRepositorio == null)
                {
                    _empresaRepositorio = new Repositorio<Empresa>(_contexto);
                }
                return _empresaRepositorio;
            }
        }

        public IRepositorio<Funcionario> FuncionarioRepositorio
        {
            get
            {
                if(_funcionarioRepositorio == null)
                {
                    _funcionarioRepositorio = new Repositorio<Funcionario>(_contexto);
                }
                return _funcionarioRepositorio;
            }
        }
        public IRepositorio<Cargo> CargoRepositorio
        {
            get
            {
                if(_cargoRepositorio == null)
                {
                    _cargoRepositorio = new Repositorio<Cargo>(_contexto);
                }
                return _cargoRepositorio;
            }
        }
        public IRepositorio<FuncionarioCargo> FuncionarioCargoRepositorio
        {
            get
            {
                if(_funcionarioCargoRepositorio == null)
                {
                    _funcionarioCargoRepositorio = new Repositorio<FuncionarioCargo>(_contexto);
                }
                return _funcionarioCargoRepositorio;
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
