using Application.Exceptions;
using Newtonsoft.Json;

namespace AuthenticationApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            // Handle the exception
            await HandleDomainExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            // Handle the exception
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleDomainExceptionAsync(HttpContext context, DomainException exception)
    {
        // Log the exception or perform any other custom handling
        
        // Set the response status code
        context.Response.StatusCode = (int)exception.StatusCode;

        // Write a JSON response with the error message
        context.Response.ContentType = "application/json";
        var errorMessage = JsonConvert.SerializeObject(new { error = exception.Message });
        await context.Response.WriteAsync(errorMessage);
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception or perform any other custom handling
        
        // Set the response status code
        context.Response.StatusCode = 500;

        // Write a JSON response with the error message
        context.Response.ContentType = "application/json";
        var errorMessage = JsonConvert.SerializeObject(new { error = exception.Message });
        await context.Response.WriteAsync(errorMessage);
    }
}
