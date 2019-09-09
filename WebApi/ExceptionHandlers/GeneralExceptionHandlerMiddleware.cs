using Common.Loggers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebApi.ExceptionHandlers
{
    public class GeneralExceptionHandlerMiddleware
    {
        protected RequestDelegate Next { get; }

        protected ILogger Logger { get; }

        public GeneralExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
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
            catch (Exception ex)
            {
                Logger.Error(ex);
                context.Response.Clear();
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Message));
            }
        }
    }
}
