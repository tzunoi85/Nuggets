using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Common.Attributes;
using Common.Loggers;
using Common.Providers;


namespace Cqs.Pipelines
{
    public class LoggingPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        protected ILogger Logger { get; }

        protected IUserProvider UserProvider { get; }

        public LoggingPipelineBehavior(ILogger logger, IUserProvider userProvider)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            UserProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestType = request.GetType();
            var skipLoggingRequest = requestType.GetCustomAttributes(typeof(SkipLoggingAttribute)).Any();

            Logger.Debug($"User:{UserProvider.GetCurrentUserId()}, " +
                $"request type: {requestType}, " +
                $"request value: {(skipLoggingRequest ? string.Empty : JsonConvert.SerializeObject(request, Formatting.Indented))}");

            var response = await next();

            var responseType = response.GetType();
            var skipLoggingResponse = responseType.GetCustomAttributes(typeof(SkipLoggingAttribute)).Any();

            Logger.Debug($"User:{UserProvider.GetCurrentUserId()}," +
                $"response type: {response.GetType()}, " +
                $"response value: {(skipLoggingResponse ? string.Empty : JsonConvert.SerializeObject(response, Formatting.Indented))}");

            return response;
        }
    }
}
