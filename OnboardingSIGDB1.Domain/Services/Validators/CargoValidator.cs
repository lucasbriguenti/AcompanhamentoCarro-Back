using FluentValidation;
using OnboardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.Domain.Services.Validators
{
    public class CargoValidator : AbstractValidator<Cargo>
    {
        public CargoValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(250).WithMessage("Descrição inválida");
        }
    }
}
