using System.Text.Json;
using Backend.Application.Common;
using Backend.Domain.Exceptions;

namespace Backend.Api.Middleware;

/// <summary>
/// Translates domain exceptions into the uniform <see cref="ErrorResponse"/> shape.
/// Registered first in the pipeline so it wraps everything downstream.
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly JsonSerializerOptions _json;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        JsonSerializerOptions json)
    {
        _next = next;
        _logger = logger;
        _json = json;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await WriteAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (DomainException ex)
        {
            await WriteAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteAsync(context, StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private async Task WriteAsync(HttpContext context, int status, string message)
    {
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";
        var body = new ErrorResponse(message, status);
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, _json));
    }
}
