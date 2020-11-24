using FluentValidation;
using OnBoardingSIGDB1.Models.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Services.Validators
{
    public class RegistroKilometragemValidator : AbstractValidator<RegistroKilometragem>
    {
        public RegistroKilometragemValidator()
        {
            RuleFor(x => x.CarroId).GreaterThan(0);
            RuleFor(x => x.FuncionarioId).NotEmpty();
        }
    }
}
