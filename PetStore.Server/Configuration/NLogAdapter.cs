using NLog;
using PetStore.Common.Models;
using PetStore.Server.Helpers;

namespace PetStore.Server.Configuration;

/// <summary>
/// Provides adapted NLog logging
/// </summary>
public class NLogAdapter : ILoggerAdapter
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Logs exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ex"></param>
    /// <param name="request"></param>
    /// <param name="severity"></param>
    public void LogException<T>(Exception ex, T request, Severity severity = Severity.Error)
    {
        _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
    }

    /// <summary>
    /// Logs exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ex"></param>
    /// <param name="severity"></param>
    public void LogException<T>(Exception ex, Severity severity = Severity.Error)
    {
        _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ex"></param>
    /// <param name="request"></param>
    public void LogException<T>(BaseException ex, T request)
    {
        _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, request);
    }

    /// <summary>
    /// Logs exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ex"></param>
    public void LogException<T>(BaseException ex)
    {
        _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, null);
    }

    /// <summary>
    /// Logs message in console
    /// </summary>
    /// <param name="message"></param>
    public void LogDebug(string message)
    {
        _logger.Debug(message);
    }

    private static NLog.LogLevel ConvertToNLogLevel(Severity severity)
    {
        switch (severity)
        {
            case Severity.Information:
                return NLog.LogLevel.Info;
            case Severity.Critical:
                return NLog.LogLevel.Fatal;
            case Severity.Warning:
                return NLog.LogLevel.Warn;
            case Severity.Debug:
                return NLog.LogLevel.Debug;
            case Severity.Error:
            default:
                return NLog.LogLevel.Error;
        }
    }
}
