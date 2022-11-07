using PetStore.Common.Models;

namespace PetStore.Server.Helpers
{
    public interface ILoggerAdapter
    {
        void LogException<T>(Exception ex, T request, Severity severity = Severity.Error);
        void LogException<T>(Exception ex, Severity severity = Severity.Error);
        void LogException<T>(BaseException ex, T request);
        void LogException<T>(BaseException ex);
        void LogDebug(string message);
    }
}
