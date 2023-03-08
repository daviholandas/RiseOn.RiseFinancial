using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace RiseOn.RiseFinancial.Infrastructure.Data.Queries.Category;

public class CategoryQueryHandler : 
    IQueryHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryQueryResult>>>,
    IQueryHandler<GetCategoryByIdQuery, Result<CategoryQueryResult>>

{
    private readonly RiseFinancialDbContext _dbContext;

    public CategoryQueryHandler(RiseFinancialDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result<IEnumerable<CategoryQueryResult>>> Handle(
        GetAllCategoriesQuery query,
        CancellationToken cancellationToken)
        => Result<IEnumerable<CategoryQueryResult>>
            .Success(await _dbContext.Categories
            .AsNoTracking()
            .Select(x => new CategoryQueryResult(x.Id, x.Name))
            .ToListAsync(cancellationToken));

    public async ValueTask<Result<CategoryQueryResult>> Handle(
        GetCategoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new CategoryQueryResult(x.Id, x.Name))
            .FirstOrDefaultAsync(cancellationToken);
        
        return category is null ?
            Result<CategoryQueryResult>.NotFound() : 
            Result<CategoryQueryResult>.Success(category);
    }
}