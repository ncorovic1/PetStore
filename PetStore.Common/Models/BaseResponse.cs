namespace PetStore.Common.Models
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public Error Error { get; set; }
        public Status Status { get; set; } = Status.Success;
    }

    public class Error
    {
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public Severity Severity { get; set; }
        public string Code { get; set; }
    }

    public enum Status {
        Fail = 0,
        Success = 1,
    }

    public enum Severity
    {
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }
}
