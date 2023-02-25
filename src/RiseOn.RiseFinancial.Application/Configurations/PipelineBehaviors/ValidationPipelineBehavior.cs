using Ardalis.Result;
using Mediator;

namespace RiseOn.RiseFinancial.Application.Configurations.PipelineBehaviors;

public class ValidationPipelineBehavior<TMessage,TResponse> 
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IRequest
    where TResponse : Result
{
    public ValueTask<TResponse> Handle(TMessage message, 
        CancellationToken cancellationToken, 
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        throw new NotImplementedException();
    }
}