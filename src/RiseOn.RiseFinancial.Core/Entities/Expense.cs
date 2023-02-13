using System.ComponentModel;
using RiseOn.Common.Domain.Record;

namespace RiseOn.RiseFinancial.Core.Entities;

public record Expense : Entity
{
    public Expense(decimal value, string? description,
        string recipient, PaymentType paymentType, 
        string category)
    {
        Value = value;
        Description = description;
        Recipient = recipient;
        PaymentType = paymentType;
        Category = category;
    }

    public decimal Value { get; private set; }

    public string? Description { get; private set; }

    public string Recipient { get; private set; }

    public PaymentType PaymentType { get; private set; }

    public Category Category { get; private set; }

}

public enum PaymentType
{
    Credit,
    Debit
}