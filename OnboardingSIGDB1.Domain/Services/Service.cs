using FluentValidation;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.Domain.Services
{
    public class Service<T> : ServiceBase<T> where T : Entity
    {
        public Service(IUnitOfWork<T> uow, AbstractValidator<T> validator, NotificationContext notification) : base(uow, validator, notification)
        {
            
        }
    }
}
