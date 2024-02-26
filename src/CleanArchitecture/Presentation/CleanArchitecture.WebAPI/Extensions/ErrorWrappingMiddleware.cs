using System.Text.Json;
using Shared.SeedWork;
using ILogger = Serilog.ILogger;

namespace CleanArchitecture.WebAPI.Extensions;

public class ErrorWrappingMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ErrorWrappingMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        var errorMsg = string.Empty;
        var exception = new Exception();
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            errorMsg = ex.Message;
            exception = ex;
            context.Response.StatusCode = 500;
        }

        if (!context.Response.HasStarted && (context.Response.StatusCode == StatusCodes.Status401Unauthorized) ||
            context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResult
            {
                Messages = "Unauthorized",
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

        else if (!context.Response.HasStarted && context.Response.StatusCode != StatusCodes.Status204NoContent &&
                 context.Response.StatusCode != StatusCodes.Status202Accepted &&
                 context.Response.StatusCode != StatusCodes.Status200OK &&
                 context.Response.ContentType != "text/html; charset=utf-8")
        {
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResult
            {
                Messages = errorMsg,
                Exception = exception
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}