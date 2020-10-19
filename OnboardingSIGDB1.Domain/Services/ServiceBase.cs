using FluentValidation;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Services
{
    public abstract class ServiceBase<T> : IService<T> where T : Entity
    {
        protected readonly IUnitOfWork<T> _uow;
        protected readonly AbstractValidator<T> validator;
        protected readonly NotificationContext _notification;

        protected ServiceBase(IUnitOfWork<T> uow, AbstractValidator<T> validator, NotificationContext notification)
        {
            _uow = uow;
            this.validator = validator;
            _notification = notification;
        }

        public virtual bool Armazenar(T obj, int? id = null)
        {
            return id.HasValue ? Atualizar(obj, id.Value) : Adicionar(obj);
        }

        public async Task<int> Commit()
        {
            return await _uow.Commit();
        }

        public bool Excluir(int id)
        {
            var obj = _uow.Repositorio.Get(x => x.Id == id);
            if (obj == null)
            {
                _notification.AddNotifications(obj);
                return false;
            }

            _uow.Repositorio.Deletar(obj);
            return true;
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _uow.Repositorio.GetAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetTudo(Expression<Func<T, bool>> predicate = null)
        {
            return await _uow.Repositorio.GetTudoAsync(predicate);
        }
        private bool Atualizar(T obj, int id)
        {
            var objRecuperado = _uow.Repositorio.Get(x => x.Id == id);
            if (objRecuperado == null || id != obj.Id)
            {
                _notification.AddNotification("0", "Não encontrado");
                return false;
            }

            if (!obj.Validate(obj, validator))
            {
                _notification.AddNotifications(obj);
                return false;
            }

            _uow.Repositorio.Atualizar(obj);
            return true;
        }
        private bool Adicionar(T obj)
        {
            if (!obj.Validate(obj, validator))
            {
                _notification.AddNotifications(obj);
                return false;
            }

            _uow.Repositorio.Adicionar(obj);
            return true;
        }
    }
}
