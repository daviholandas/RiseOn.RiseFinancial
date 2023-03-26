using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Infra.Data.Graphql.Queries;

public class CategoryQuery
{
    public IQueryable<Category> Categories(RiseFinancialDbContext dbContext)
        => dbContext.Categories.AsNoTracking();
}