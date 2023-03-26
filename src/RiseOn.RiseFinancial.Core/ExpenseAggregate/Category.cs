using Ardalis.GuardClauses;
using Entity = RiseOn.Common.Domain.Record.Entity;

namespace RiseOn.RiseFinancial.Core.ExpenseAggregate;

public record Category : Entity
{
    public Category(string name)
        => Name = name; //Guard.Against.NullOrWhiteSpace(name, nameof(Name));

    public string Name { get; init; }

    public IEnumerable<Expense> Expenses { get; init; } = Enumerable.Empty<Expense>();
}