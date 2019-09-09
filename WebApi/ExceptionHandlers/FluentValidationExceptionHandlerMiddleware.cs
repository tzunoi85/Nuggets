using Common.Exceptions;
using Common.Loggers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebApi.ExceptionHandlers
{
    public class FluentValidationExceptionHandlerMiddleware
    {
        protected RequestDelegate Next { get; }

        protected ILogger Logger { get; }

        public FluentValidationExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            Next = next ?? throw new ArgumentNullException(next.GetType().ToString());
            Logger = logger ?? throw new ArgumentNullException(logger.GetType().ToString());
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (DataValidationException ex)
            {
                Logger.Error(ex);
                context.Response.Clear();
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Errors));
            }
        }

    }
}
