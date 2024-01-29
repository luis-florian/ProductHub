using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductHub.Database.Model;
using ProductHub.Model;
using System.Net;

namespace ProductHub.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (MissingRelatedEntityException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";

                await context.Response.WriteAsync($"Not Found: {ex.Message}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = $"Internal Server Error: {ex.Message}"
                }.ToString());
            }
        }
    }
}
