using Ardalis.Result;
using Mediator;
using ExpenseAggregate = RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Application.UseCases.Expense;

public class CreateFixedExpenseHandler : ICommandHandler<CreateFixedExpenseCommand, Result<Guid>>
{

    public ValueTask<Result<Guid>> Handle(CreateFixedExpenseCommand command,
        CancellationToken cancellationToken)
    {
        ExpenseAggregate.Expense expense = new(
            command.Value, command.Description, 
            command.Recipient, command.Category,
            command.WalletId, command.Installment);

        return ValueTask.FromResult(Result<Guid>.Success(expense.Id));
    }
}


public record struct CreateFixedExpenseCommand(
        decimal Value, string? Description,
        string Recipient, string Category,
        Guid WalletId, int Installment)
    : ICommand<Result<Guid>>;