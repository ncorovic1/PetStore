using FluentValidation;

namespace PetStore.BLL.Toys.Commands
{
    public class CreateToyCommandValidator : AbstractValidator<CreateToyCommand>
    {
        public CreateToyCommandValidator
        (
        )
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(100)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");

            RuleFor(x => x.TypeId)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");
        }
    }
}
