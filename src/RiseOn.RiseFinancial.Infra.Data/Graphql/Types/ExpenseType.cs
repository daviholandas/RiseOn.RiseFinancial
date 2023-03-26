using RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Infra.Data.Graphql.Types;


public class ExpenseType : ObjectTypeExtension<Expense>
{
    protected override void Configure(IObjectTypeDescriptor<Expense> descriptor)
    {
        base.Configure(descriptor);
    }
}