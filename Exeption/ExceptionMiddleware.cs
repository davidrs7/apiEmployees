using System.Net;
using System.Text.Json;

namespace Api.Exeption
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Pasa al siguiente middleware en la cadena
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Algo salió mal: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error interno del servidor. Por favor, inténtalo de nuevo más tarde."
            });

            return context.Response.WriteAsync(result);
        }
    }

}
