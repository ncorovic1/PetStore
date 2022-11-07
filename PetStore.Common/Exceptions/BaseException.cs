namespace PetStore.Common.Models;

public class BaseException : Exception
{
    public BaseException()
    {
    }

    public BaseException(string message)
        : base(message)
    {
    }

    public BaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public BaseException(string message, Severity severity)
        : base(message)
    {
        this.Severity = severity;
    }

    public BaseException(string message, Exception innerException, Severity severity)
        : base(message, innerException)
    {
        this.Severity = severity;
    }

    public Severity Severity { get; }
}
