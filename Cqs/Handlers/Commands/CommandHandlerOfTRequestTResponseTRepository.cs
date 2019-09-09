using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

using Common.Repositories;


namespace Cqs.Handlers.Commands
{
    public abstract class CommandHandler<TRequest, TResponse, TRepository>
        : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TRepository : class, IRepository
    {
        protected IMapper Mapper { get; set; }

        protected TRepository Repository { get; set; }

        public CommandHandler(IMapper mapper, TRepository repository)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
