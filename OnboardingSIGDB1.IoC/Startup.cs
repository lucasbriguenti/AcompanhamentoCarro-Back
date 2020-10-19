using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Models.Classes;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Services.Funcionarios;
using OnboardingSIGDB1.Domain.Services.Validators;

namespace OnboardingSIGDB1.IoC
{
    public static class Startup
    {
        public static void MapeamentoGenerico(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<Funcionario>, UnitOfWork<Funcionario>>();
            services.AddScoped<IUnitOfWork<Cargo>, UnitOfWork<Cargo>>();
            services.AddScoped<IUnitOfWork<Empresa>, UnitOfWork<Empresa>>();
            services.AddScoped<IUnitOfWork<FuncionarioCargo>, UnitOfWork<FuncionarioCargo>>();


            services.AddScoped<IService<Empresa>, Service<Empresa>>();
            services.AddScoped<IService<Cargo>, Service<Cargo>>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();

            services.AddScoped<NotificationContext>();

            services.AddScoped<AbstractValidator<Cargo>, CargoValidator>();
            services.AddScoped<AbstractValidator<Empresa>, EmpresaValidator>();
            services.AddScoped<AbstractValidator<Funcionario>, FuncionarioValidator>();
            services.AddScoped<AbstractValidator<FuncionarioCargo>, FuncionarioCargoValidator>();

        }
    }
}
