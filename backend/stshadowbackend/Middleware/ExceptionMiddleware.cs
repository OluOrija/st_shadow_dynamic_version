using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace stshadowbackend.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            var response = new
            {
                message = exception.Message,
                details = context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()
                    ? exception.StackTrace
                    : null // Hide stack trace in production
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

    }

}
