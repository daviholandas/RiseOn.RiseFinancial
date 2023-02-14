using RiseOn.Common.Domain;
using Entity = RiseOn.Common.Domain.Record.Entity;

namespace RiseOn.RiseFinancial.Core.ExpenseAggregate;

public record Expense : Entity, IAggregateRoot
{
    public Expense(decimal value, string? description,
        string recipient, PaymentType paymentType,
        string category, Guid walletId)
    {
        Value = value;
        Description = description;
        Recipient = recipient;
        PaymentType = paymentType;
        Category = category;
        WalletId = walletId;
        Status = Status.Open;
    }

    public decimal Value { get; private set; }

    public string? Description { get; private set; }

    public string Recipient { get; private set; }

    public PaymentType PaymentType { get; init; }

    public Category Category { get; private set; }

    public Guid WalletId { get; private set; }

    public Status Status { get; private set; }

    public int InstallmentNumber { get; init; } = 1;

    public void Postpone()
        => Status = Status.Postponed;

    public void Settle()
        => Status = Status.Payed;

    public void ChangeCategory(Category category)
        => Category = category;
}