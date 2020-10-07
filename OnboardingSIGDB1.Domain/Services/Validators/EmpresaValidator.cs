﻿using FluentValidation;
using OnboardingSIGDB1.Domain.Dto;
using System;

namespace OnboardingSIGDB1.Domain.Services.Validators
{
    public class EmpresaValidator : AbstractValidator<EmpresaDto>
    {
        public EmpresaValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome obrigatório");
            RuleFor(x => x.DataFundacao).GreaterThan(DateTime.MinValue).WithMessage($"Campo Data de Fundação deve ser maior que {DateTime.MinValue.ToShortDateString()}");
            RuleFor(x => x.Cnpj).IsValidCNPJ().WithMessage("CNPJ inválido");
        }
    }
}
