using Ardalis.Result;
using Mediator;
using ExpenseAggregate = RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Application.Commands.Expense;

public record struct CreateFixedExpenseCommand(
        decimal Value, string? Description,
        string Recipient, string Category,
        Guid? WalletId, int Installment)
    : ICommand<Result<Guid>>;

public record struct CreateVariableExpenseCommand(
        decimal Value, string? Description,
        string Recipient, string Category,
        Guid WalletId)
    : ICommand<Result<Guid>>;


public class CreateExpensesHandler : 
    ICommandHandler<CreateFixedExpenseCommand, Result<Guid>>, 
    ICommandHandler<CreateVariableExpenseCommand, Result<Guid>>
{

    public ValueTask<Result<Guid>> Handle(CreateFixedExpenseCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<Guid>> Handle(CreateVariableExpenseCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
