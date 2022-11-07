using FluentValidation;

namespace PetStore.BLL.Toys.Commands
{
    public class PatchToyCommandValidator : AbstractValidator<PatchToyCommand>
    {
        public PatchToyCommandValidator
        (
        )
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");

            When(x => x.Name is { DoUpdate: true }, () =>
            {
                RuleFor(x => x.Name.Value)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage("The {PropertyName} passed should not be null or empty")
                    .MaximumLength(100)
                    .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");
            });

            When(x => x.CategoryId is { DoUpdate: true }, () =>
            {
                RuleFor(x => x.CategoryId.Value)
                    .GreaterThan(0)
                    .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");
            });

            When(x => x.TypeId is { DoUpdate: true }, () =>
            {
                RuleFor(x => x.TypeId.Value)
                    .GreaterThan(0)
                    .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");
            });

            When(x => x.Price is { DoUpdate: true }, () =>
            {
                RuleFor(x => x.Price.Value)
                    .GreaterThan(0)
                    .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");
            });

            When(x => x.Quantity is { DoUpdate: true }, () =>
            {
                RuleFor(x => x.Quantity.Value)
                    .GreaterThan(0)
                    .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");
            });
        }
    }
}
