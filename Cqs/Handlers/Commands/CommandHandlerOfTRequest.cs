using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

using Common.Repositories;


namespace Cqs.Handlers.Commands
{
    public abstract class CommandHandler<TRequest>
        : AsyncRequestHandler<TRequest>
        where TRequest : IRequest
    {
        protected IMapper Mapper { get; set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        public CommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }
}
