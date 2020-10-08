using FluentValidation;
using OnboardingSIGDB1.Domain.Dto;

namespace OnboardingSIGDB1.Domain.Services.Validators
{
    public class CargoValidator : AbstractValidator<CargoDto>
    {
        public CargoValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(250).WithMessage("Descrição inválida");
        }
    }
}
