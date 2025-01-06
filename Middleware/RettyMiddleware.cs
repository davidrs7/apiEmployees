namespace MiddlewareRetryMiddleware
{


    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using MySqlConnector;

    public class RetryMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RetryMiddleware> _logger;
        private readonly int _maxRetries;
        private readonly TimeSpan _baseDelay;

        public RetryMiddleware(RequestDelegate next, ILogger<RetryMiddleware> logger, int maxRetries = 5, int baseDelayInSeconds = 2)
        {
            _next = next;
            _logger = logger;
            _maxRetries = maxRetries;
            _baseDelay = TimeSpan.FromSeconds(baseDelayInSeconds);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            int attempt = 0;

            while (true)
            {
                try
                {
                    await _next(context); // Llamar al siguiente middleware o al controlador
                    return; // Salir si la solicitud fue exitosa
                }
                catch (Exception ex) when (IsTransient(ex))
                {
                    attempt++;
                    if (attempt > _maxRetries)
                    {
                        // Si se superan los intentos, registrar y responder con un error
                        _logger.LogError(ex, "Se alcanzó el máximo de reintentos.");
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync("Error en la conexión, por favor intente más tarde.");
                        return;
                    }

                    // Esperar antes de reintentar (retraso exponencial)
                    var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt) * _baseDelay.TotalSeconds);
                    _logger.LogWarning($"Reintentando... Intento {attempt} de {_maxRetries}. Error: {ex.Message}");
                    await Task.Delay(delay);
                }
            }
        }

        private bool IsTransient(Exception ex)
        {
            // Manejo de excepciones transitorias específicas
            if (ex is MySqlException mysqlEx)
            {
                return mysqlEx.Number == 1205 || // Lock wait timeout
                       mysqlEx.Number == 2002 || // Can't connect to local MySQL server
                       mysqlEx.Number == 2013 || // Lost connection during query
                       mysqlEx.Number == 2006;    // MySQL server has gone away
            }
            // Agrega lógica para otras excepciones si es necesario
            return false; // Cambia esto según tus necesidades
        }
    }

}