using RiseOn.Common.Domain.Record;

namespace RiseOn.RiseFinancial.Core.Entities;

public record Budget : Entity
{
    private HashSet<Earning> earnings = new();

    public IReadOnlyCollection<Earning> Earnings 
        => earnings;
}