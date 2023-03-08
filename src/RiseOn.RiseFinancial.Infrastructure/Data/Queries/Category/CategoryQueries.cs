using Ardalis.Result;
using Mediator;

namespace RiseOn.RiseFinancial.Infrastructure.Data.Queries.Category;

public record struct GetAllCategoriesQuery()
    : IQuery<Result<IEnumerable<CategoryQueryResult>>>;
    
    
public record struct GetCategoryByIdQuery(Guid Id)
    : IQuery<Result<CategoryQueryResult>>;