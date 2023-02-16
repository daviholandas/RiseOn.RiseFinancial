using Ardalis.GuardClauses;
using RiseOn.Common.Domain;
using RiseOn.RiseFinancial.Core.ExpenseAggregate.Events;
using Entity = RiseOn.Common.Domain.Record.Entity;

namespace RiseOn.RiseFinancial.Core.ExpenseAggregate;

public record Expense : Entity, IAggregateRoot
{
    public Expense(decimal value, string? description,
        string recipient, string category,
        Guid walletId)
    {
        Value = Guard.Against.NegativeOrZero(value, nameof(Value));
        Description = description;
        Recipient = recipient;
        ExpenseType = ExpenseType.Variable;
        Category = category;
        WalletId = Guard.Against.NullOrEmpty(walletId, nameof(WalletId));
        Status = Status.Open;
    }
    
    public Expense(decimal value, string? description,
        string recipient, string category,
        Guid walletId, int installmentNumber)
    : this(value, description, recipient,
        category, walletId)
    {
        ExpenseType = ExpenseType.Fixed;
        InstallmentNumber = installmentNumber;
    }

    public decimal Value { get; private set; }

    public string? Description { get; private set; }

    public string Recipient { get; private set; }

    public ExpenseType ExpenseType { get; init; }

    public Category Category { get; private set; }

    public Guid WalletId { get; private set; }

    public Status Status { get; private set; }

    public int InstallmentNumber { get; init; } = 1;

    public void Postpone()
        => Status = Status.Postponed;

    public async ValueTask SettleAsync()
    {
        Status = Status.Payed;
        await SendDomainEvent(new ExpenseSettledEvent(WalletId,
            Value, Id));
    }

    public void ChangeCategory(Category category)
        => Category = category;
}