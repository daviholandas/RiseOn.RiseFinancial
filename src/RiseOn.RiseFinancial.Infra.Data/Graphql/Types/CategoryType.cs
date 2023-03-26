using RiseOn.RiseFinancial.Core.ExpenseAggregate;


namespace RiseOn.RiseFinancial.Infra.Data.Graphql.Types;

public class CategoryType : ObjectType<Category>
{
    protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
    {
        descriptor.Ignore(x => x.Expenses);
        descriptor.Ignore(x => x.CreateAt);
    }
}