using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PetStore.Common.Models;

namespace PetStore.Server.Configuration;

/// <summary>
/// Mediatr validation handler
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="validators"></param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="PSValidationException"></exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);

        var tasks = new List<Task<ValidationResult>>();
        foreach (var validator in _validators)
        {
            tasks.Add(validator.ValidateAsync(context, cancellationToken));
        }

        var validationResults = await Task.WhenAll(tasks);

        var failures = new List<ValidationFailure>();
        foreach (var validationResult in validationResults)
        {
            var errors = validationResult.Errors;
            foreach (var error in errors)
            {
                if (error != null)
                {
                    failures.Add(new ValidationFailure
                    {
                        ErrorMessage = error.ErrorMessage,
                        PropertyName = error.PropertyName
                    });
                }
            }
        }

        if (failures.Count > 0) throw new PSValidationException(failures);

        return await next();
    }
}
