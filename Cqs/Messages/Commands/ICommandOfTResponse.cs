using MediatR;

using Cqs.Validations;


namespace Cqs.Messages.Commands
{
    public interface ICommand<TResponse>
        : IRequest<TResponse>, IValidatableRequest
    {
    }
}
