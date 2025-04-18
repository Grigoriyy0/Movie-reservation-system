using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MovieReservationSystem.Presentation.Middlewares;

public class ValidationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationExceptionHandlerMiddleware> _logger;

    public ValidationExceptionHandlerMiddleware(RequestDelegate next, ILogger<ValidationExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex.ToString());

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred.",
                Detail = ex.Message,
            };
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}