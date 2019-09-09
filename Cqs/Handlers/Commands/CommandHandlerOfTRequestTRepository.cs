using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

using Common.Repositories;


namespace Cqs.Handlers.Commands
{
    public abstract class CommandHandler<TRequest, TRepository>
        : AsyncRequestHandler<TRequest>
        where TRequest : IRequest
        where TRepository : class, IRepository
    {

        protected IMapper Mapper { get; set; }

        protected TRepository Repository { get; set; }

        public CommandHandler(IMapper mapper, TRepository repository)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }
}
