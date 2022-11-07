namespace PetStore.Common.Models
{ 
    public class PSNotFoundException : BaseException
    {
        public PSNotFoundException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public PSNotFoundException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
    }
}
