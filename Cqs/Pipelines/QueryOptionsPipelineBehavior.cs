using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Common.OData;
using Common.Loggers;

namespace Cqs.Pipelines
{
    public class QueryOptionsPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TResponse : IQueryable

    {
        public QueryOptionsPipelineBehavior(IQueryOptions queryOptions, ILogger logger)
        {
            QueryOptions = queryOptions ?? throw new ArgumentNullException(nameof(queryOptions));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        protected IQueryOptions QueryOptions { get; }

        protected ILogger Logger { get; }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Logger.Information($"Apply query options!");
            return (TResponse)await QueryOptions.ApplyQuery(await next());
        }

    }
}
