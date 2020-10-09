using FluentValidation;
using OnboardingSIGDB1.Domain.Models;
using System;

namespace OnboardingSIGDB1.Domain.Services.Validators
{
    public class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        public FuncionarioValidator()
        {
            RuleFor(x => x.Cpf).IsValidCPF().NotEmpty().MaximumLength(14).WithMessage("CPF Inválido");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome obrigatório");
            RuleFor(x => x.Nome).MaximumLength(150).WithMessage("Nome deve ter menos que 150 caracteres");
            RuleFor(x => x.DataContratacao).GreaterThan(DateTime.MinValue).WithMessage($"Data de contratacao deve ser maior que {DateTime.MinValue.ToShortDateString()}");
        }
    }
}
