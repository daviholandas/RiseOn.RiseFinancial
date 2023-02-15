using Ardalis.Result;
using Mediator;

namespace RiseOn.RiseFinancial.Application.Commands.Expense;

public record struct CreateFixedExpenseCommand(
        decimal Value, string? Description,
        string Recipient, string Category,
        Guid WalletId)
    : ICommand<Result<Guid>>;