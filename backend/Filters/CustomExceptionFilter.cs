// backend/Filters/CustomExceptionFilter.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        // You can inject ILogger here if you have a logging setup
        // private readonly ILogger<CustomExceptionFilter> _logger;
        // public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        // {
        //     _logger = logger;
        // }

        public void OnException(ExceptionContext context)
        {
            IActionResult? result = null;
            var exception = context.Exception;

            // Log the exception (recommended for all exceptions)
            // _logger.LogError(exception, $"An unhandled exception occurred: {exception.Message}");

            if (exception is KeyNotFoundException)
            {
                result = new NotFoundObjectResult(exception.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (exception is UnauthorizedAccessException)
            {
                result = new ForbidResult(); // Returns 403 Forbidden
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else if (exception is FormatException || exception is InvalidOperationException)
            {
                result = new BadRequestObjectResult(exception.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                // Generic 500 Internal Server Error for unhandled exceptions
                result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                // Or return a more specific error object:
                // result = new ObjectResult($"An unexpected error occurred: {exception.Message}")
                // {
                //     StatusCode = StatusCodes.Status500InternalServerError
                // };
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Result = result;
            context.ExceptionHandled = true; // Mark exception as handled
        }
    }
}
