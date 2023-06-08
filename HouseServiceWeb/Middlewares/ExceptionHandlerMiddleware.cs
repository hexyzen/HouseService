using HouseService.Managers.Exceptions;
using System.Net;

namespace HouseService.Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        private const string StandartResponseMessage = "Internal server error";
        private const HttpStatusCode StandartHttpStatusCode = HttpStatusCode.InternalServerError;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound, responseMessage: ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message,
                    responseMessage: string.IsNullOrEmpty(ex.Message)
                        ? StandartResponseMessage
                        : ex.Message);
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, string errorMessage,
                                                        HttpStatusCode httpStatusCode = StandartHttpStatusCode, string responseMessage = StandartResponseMessage)
        {
            _logger.LogWarning(errorMessage);

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)httpStatusCode;

            await context.Response.WriteAsync(responseMessage);
        }
    }
}
