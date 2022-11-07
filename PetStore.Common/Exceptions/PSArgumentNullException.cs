namespace PetStore.Common.Models
{ 
    public class PSArgumentNullException : BaseException
    {
        public PSArgumentNullException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public PSArgumentNullException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
    }
}
