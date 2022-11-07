using FluentValidation.Results;

namespace PetStore.Common.Models
{ 
    public class PSValidationException : BaseException
    {
		/// <summary>
		/// Validation errors
		/// </summary>
		public IEnumerable<ValidationFailure> Errors { get; private set; }

		/// <summary>Creates a new ValidationException</summary>
		/// <param name="message">Exception message</param>
		/// <param name="severity">Exception severity</param>
		public PSValidationException(string message, Severity severity = Severity.Error) : this(message, Enumerable.Empty<ValidationFailure>(), severity)
		{
		}

		/// <summary>Creates a new ValidationException</summary>
		/// <param name="message">Exception message</param>
		/// <param name="errors">Key value errors</param>
		/// <param name="severity">Exception severity</param>
		public PSValidationException(string message, IEnumerable<ValidationFailure> errors, Severity severity) : base(message, severity)
		{
			Errors = errors;
		}

		/// <summary>Creates a new ValidationException</summary>
		/// <param name="message">Exception message</param>
		/// <param name="inner">Inner exception</param>
		/// <param name="severity">Exception severity</param>
		public PSValidationException(string message, Exception inner, Severity severity = Severity.Error) : base(message, inner, severity)
		{
		}

		/// <summary>Creates a new ValidationException</summary>
		/// <param name="errors">Key value errors</param>
		/// <param name="severity">Exception severity</param>
		public PSValidationException(IEnumerable<ValidationFailure> errors, Severity severity = Severity.Error) : this(BuildErrorMessage(errors), severity)
		{
			Errors = errors;
		}

		private static string BuildErrorMessage(IEnumerable<ValidationFailure> errors)
		{
			var arr = errors.Select(x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage} Severity: {x.Severity}");
			return "Validation failed: " + string.Join(string.Empty, arr);
		}
	}
}