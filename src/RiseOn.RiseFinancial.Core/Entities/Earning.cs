using RiseOn.Common.Domain.Record;

namespace RiseOn.RiseFinancial.Core.Entities;

public record Earning : Entity
{
    public Earning(decimal value,
        EarningSources source,
        string? description = null)
    {
        Value = value;
        Source = source;
        Description = description;
    }

    public decimal Value { get; private set; }

    public string? Description { get; private set; }

    public EarningSources Source { get; private set; }
}

public enum EarningSources
{
    Salary,
    Extra,
    Job,
}