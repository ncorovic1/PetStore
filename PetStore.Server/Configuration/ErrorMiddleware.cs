using Newtonsoft.Json;
using PetStore.Common.Models;
using PetStore.Server.Helpers;
using System.Net;

namespace PetStore.Server.Configuration;

/// <summary>
/// Exception interceptor
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Interceptor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, ILoggerAdapter logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger);
        }
    }

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    protected static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILoggerAdapter logger)
    {
        var exceptionType = exception.GetType();
        var severity = (exception as BaseException)?.Severity ?? Severity.Critical;
        logger.LogException(exception, context.Request, severity);

        context.Response.ContentType = "application/json";

        switch (exceptionType)
        {
            case Type _ when exceptionType == typeof(PSNotFoundException):
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(CreateResponseOnException(exception.Message, severity));
                break;
            case Type _ when exceptionType == typeof(PSValidationException):
                var errors = (exception as PSValidationException).Errors?.ToDictionary(x => x.PropertyName, x => x.ErrorMessage);
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                await context.Response.WriteAsync(CreateResponseOnException(exception.Message, severity, errors));
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(CreateResponseOnException("Unhandled exception", severity));
                break;
        }
    }

    private static string CreateResponseOnException(string message, Severity severity, Dictionary<string, string> errors = null)
    {
        return JsonConvert.SerializeObject(new BaseResponse<string>
        {
            Status = Status.Fail,
            Error = new Error
            {
                Severity = severity,
                Message = message,
                Errors = errors
            }
        });
    }
}
