using Mediator;

namespace RiseOn.RiseFinancial.Infra.Data.Queries.Category;

public record struct GetAllCategoriesQuery()
    : IQuery<Ardalis.Result.Result<IEnumerable<CategoryQueryResult>>>;
    
    
public record struct GetCategoryByIdQuery(Guid Id)
    : IQuery<Ardalis.Result.Result<CategoryQueryResult>>;