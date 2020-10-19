using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnBoardingSIGDB1.Models.Classes
{
	public abstract class EntityValidator
	{
		[NotMapped]
		public bool Valid { get; private set; }
		[NotMapped]
		public bool Invalid => !Valid;
		[NotMapped]
		public ValidationResult ValidationResult { get; private set; }

		public bool Validate<T>(T model, AbstractValidator<T> validator)
		{
			ValidationResult = validator.Validate(model);
			return Valid = ValidationResult.IsValid;
		}
	}
}
