using FluentValidation;
using PetStore.DataContracts.Orders;

namespace PetStore.BLL.Orders.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator
        (
        )
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(100)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(100)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.StatusId)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} passed is invalid. Set a value higher than {ComparisonValue}");

            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(50)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.StreetNumber)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(50)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.ZipCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(20)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(50)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.CreditCard)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .MaximumLength(16)
                .WithMessage("The {PropertyName} cannot be longer than {MaxLength} characters");

            RuleFor(x => x.Toys)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The {PropertyName} passed should not be null or empty")
                .Custom((x, context) =>
                {
                    var numberOfElements = x.Count;
                    var numberOfDistinctElements = x.ConvertAll(x => x.ProductId).Distinct().Count();
                    if (numberOfElements != numberOfDistinctElements)
                        context.AddFailure("At least one toy is ordered multiple times");
                });

            RuleForEach(x => x.Toys)
                .Custom((x, context) =>
                {
                    if (x.ProductId <= 0)
                    {
                        context.AddFailure($"The {nameof(OrderByToy.ProductId)} passed is invalid. Set a value higher than 0");
                        return;
                    }

                    if (x.Quantity <= 0)
                        context.AddFailure($"The {nameof(OrderByToy.Quantity)} passed is invalid. Set a value higher than 0");
                });
        }
    }
}
