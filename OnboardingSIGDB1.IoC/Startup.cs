using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Models.Classes;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Services.Validators;
using OnBoardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.IoC
{
    public static class Startup
    {
        public static void MapeamentoGenerico(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<Funcionario>, UnitOfWork<Funcionario>>();
            services.AddScoped<IUnitOfWork<Carro>, UnitOfWork<Carro>>();
            services.AddScoped<IUnitOfWork<RegistroKilometragem>, UnitOfWork<RegistroKilometragem>>();


            services.AddScoped<IService<Carro>, Service<Carro>>();
            services.AddScoped<IService<RegistroKilometragem>, Service<RegistroKilometragem>>();
            services.AddScoped<IService<Funcionario>, Service<Funcionario>>();

            services.AddScoped<NotificationContext>();

            services.AddScoped<AbstractValidator<Carro>, CarroValidator>();
            services.AddScoped<AbstractValidator<RegistroKilometragem>, RegistroKilometragemValidator>();
            services.AddScoped<AbstractValidator<Funcionario>, FuncionarioValidator>();

        }
    }
}
