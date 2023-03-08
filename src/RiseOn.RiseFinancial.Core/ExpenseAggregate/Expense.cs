using Ardalis.GuardClauses;
using RiseOn.Common.Domain;
using RiseOn.RiseFinancial.Core.ExpenseAggregate.Events;
using Entity = RiseOn.Common.Domain.Record.Entity;

namespace RiseOn.RiseFinancial.Core.ExpenseAggregate;

public record Expense : Entity, IAggregateRoot
{
    private Expense(){}
    
    public Expense(decimal value, string? description,
        string recipient, Guid categoryId,
        Guid walletId)
    {
        Value = Guard.Against.NegativeOrZero(value, nameof(Value));
        Description = description;
        Recipient = recipient;
        ExpenseType = ExpenseType.Variable;
        CategoryId = Guard.Against.NullOrEmpty(categoryId, nameof(CategoryId));
        WalletId = Guard.Against.NullOrEmpty(walletId, nameof(WalletId));
        Status = Status.Open;
        DueDay = DateTime.Today.Day;
    }
    
    public Expense(decimal value, string? description,
        string recipient, Guid categoryId,
        Guid walletId, int installmentNumber, int? dueDay)
    : this(value, description, recipient,
        categoryId, walletId)
    {
        ExpenseType = ExpenseType.Fixed;
        InstallmentNumber = installmentNumber;
        DueDay = dueDay ?? DateTime.Today.Day;
    }

    public decimal Value { get; private set; }

    public string? Description { get; private set; }

    public string? Recipient { get; private set; }

    public ExpenseType ExpenseType { get; init; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; }

    public Guid WalletId { get; private set; }

    public Status Status { get; private set; }

    public int InstallmentNumber { get; init; } = 1;

    public int DueDay { get; private set; }

    public void Postpone()
        => Status = Status.Postponed;

    public async ValueTask SettleAsync()
    {
        Status = Status.Payed;
        await SendDomainEvent(new ExpenseSettledEvent(WalletId,
            Value, Id));
    }
}