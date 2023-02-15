using Ardalis.Result;
using Mediator;
using RiseOn.RiseFinancial.Application.Commands.Expense;

namespace RiseOn.RiseFinancial.Application.UseCases.Expense;

public class CreateFixedExpenseHandler : ICommandHandler<CreateFixedExpenseCommand, Result<Guid>>
{

    public ValueTask<Result<Guid>> Handle(CreateFixedExpenseCommand command,
        CancellationToken cancellationToken)
        => throw new NotImplementedException();
}