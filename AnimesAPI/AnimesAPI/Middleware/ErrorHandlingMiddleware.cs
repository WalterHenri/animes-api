using Animes.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace AnimesAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { error = notFoundException.Message });
                    break;

                case BadRequestException badRequestException: 
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = badRequestException.Message });
                    break;

                case DuplicatedEntityException duplicatedEntityException:
                    code = HttpStatusCode.Conflict;
                    result = JsonSerializer.Serialize(new { error = duplicatedEntityException.Message });
                    break;

                default:
                    result = JsonSerializer.Serialize(new { error = "Ocorreu um erro interno no servidor." });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

