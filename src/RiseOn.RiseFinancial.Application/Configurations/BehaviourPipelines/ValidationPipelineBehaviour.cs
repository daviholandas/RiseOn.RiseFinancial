using System.Collections;
using System.Collections.Immutable;
using System.Linq.Expressions;
using Ardalis.Result;
using FluentValidation;
using Mediator;

namespace RiseOn.RiseFinancial.Application.Configurations.BehaviourPipelines;

public class ValidationPipelineBehaviour<TMessage,TResponse> 
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly Type _tMessageType = typeof(TMessage);
    private readonly IEnumerable<IValidator<TMessage>> _validators;

    public ValidationPipelineBehaviour(
        IEnumerable<IValidator<TMessage>> validators)
    {
        this._validators = validators;
    }

    public async ValueTask<TResponse> Handle(TMessage message, 
        CancellationToken cancellationToken, 
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        if (!_validators.Any())
            return await next(message, cancellationToken);

        ValidationContext<object> validationContext = new(message);

        var validatorsResult = await Task.WhenAll(
            _validators.Select(x
                => x.ValidateAsync(validationContext, cancellationToken)));

        var failureMessages = validatorsResult
            .SelectMany(e => e.Errors)
            .Where(e => e is not null)
            .Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
            .ToArray();

        return await ValueTask.FromResult(BuildResultInstance(failureMessages));
    }

    private static TResponse BuildResultInstance(IEnumerable<string> errors)
    {
        var value = Expression.Constant(errors);
        var errorMethod = typeof(TResponse).GetMethod("Error", new[] {typeof(string[])});
        var calledMethod = Expression.Call(errorMethod ?? throw new InvalidOperationException(), value);
        var expression = Expression.Lambda<Func<TResponse>>(calledMethod);
        var resultError = expression.Compile();

        return resultError();
    }
}