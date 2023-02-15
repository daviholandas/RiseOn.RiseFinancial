using RiseOn.Common.Domain;
using RiseOn.Common.Enums;

namespace RiseOn.RiseFinancial.Core.ExpenseAggregate.Events;

public record struct ExpenseSettledEvent(Guid WalletId,
    decimal Value, Guid ExpenseId) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
   
    public DateTime CreateAt { get; } = DateTime.Now;
    
    public EventStatus Status { get; } = EventStatus.Released;
}