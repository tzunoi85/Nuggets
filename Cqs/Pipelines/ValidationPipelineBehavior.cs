using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Cqs.Validations;
using Common.Attributes;
using Common.Loggers;
using Common.Exceptions;


namespace Cqs.Pipelines
{
    public class ValidationPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IValidatableRequest
    {

        public ValidationPipelineBehavior(IValidator<TRequest> validator, ILogger logger)
        {
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected IValidator<TRequest> Validator { get; }

        protected ILogger Logger { get; }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = Validator.Validate(request);
            var requestType = request.GetType();
            var skipLoggingRequest = requestType.GetCustomAttributes(typeof(SkipLoggingAttribute)).Any();

            Logger.Information($"Validation result for request: {(skipLoggingRequest ? string.Empty : JsonConvert.SerializeObject(request, Formatting.Indented))} " +
                $"is: {result.IsValid} " +
                $"{(!result.IsValid ? JsonConvert.SerializeObject(result.Errors.Select(e => e.ErrorMessage), Formatting.Indented) : string.Empty)}");

            if (!result.IsValid)
                throw new DataValidationException(result.Errors);

            return await next();
        }
    }
}