using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

using Common.Repositories;


namespace Cqs.Handlers.Queries
{
    public abstract class QueryHandler<TRequest, TResponse>
        : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected IMapper Mapper { get; set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        public QueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
