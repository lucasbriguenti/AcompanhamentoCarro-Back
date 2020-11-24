using FluentValidation;
using OnBoardingSIGDB1.Models.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Services.Validators
{
    public class CarroValidator : AbstractValidator<Carro>
    {
        public CarroValidator()
        {
            RuleFor(x => x.Marca).NotEmpty();
            RuleFor(x => x.Modelo).NotEmpty();
            RuleFor(x => x.Placa).NotEmpty();
            RuleFor(x => x.Kilometragem).NotEmpty();
        }
    }
}
