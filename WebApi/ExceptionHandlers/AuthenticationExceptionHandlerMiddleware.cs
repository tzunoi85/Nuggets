using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

using Common.Exceptions;
using Common.Loggers;

namespace WebApi.ExceptionHandlers
{
    public class AuthenticationExceptionHandlerMiddleware
    {
        protected RequestDelegate Next { get; }

        protected ILogger Logger { get; }

        public AuthenticationExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
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
            catch (RefreshTokenException ex)
            {
                await HandleError(context, ex, StatusCodes.Status401Unauthorized);
            }
            catch (UserNotFoundException ex)
            {
                await HandleError(context, ex, StatusCodes.Status404NotFound);
            }
        }

        private async Task HandleError(HttpContext context, Exception ex, int responseCode)
        {
            Logger.Error(ex);
            context.Response.Clear();
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.StatusCode = responseCode; ;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Message));
        }
    }
}
