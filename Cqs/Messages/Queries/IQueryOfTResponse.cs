using MediatR;

namespace Cqs.Messages.Queries
{
    public interface IQuery<TResponse>
        : IRequest<TResponse>
    {
    }
}
