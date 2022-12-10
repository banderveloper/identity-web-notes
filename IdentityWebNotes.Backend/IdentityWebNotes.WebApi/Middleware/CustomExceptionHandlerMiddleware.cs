using System.Net;
using System.Text.Json;
using FluentValidation;
using IdentityWebNotes.Application.Common.Exceptions;

namespace IdentityWebNotes.WebApi.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
        => _next = next;

    // Catches all exceptions which are thrown in pipeline
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, context);
        }
    }

    private Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
            result = JsonSerializer.Serialize(new { errpr = exception.Message });

        return context.Response.WriteAsync(result);
    }
}