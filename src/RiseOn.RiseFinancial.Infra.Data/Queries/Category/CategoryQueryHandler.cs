using Mediator;
using Microsoft.EntityFrameworkCore;

namespace RiseOn.RiseFinancial.Infra.Data.Queries.Category;

public class CategoryQueryHandler : 
    IQueryHandler<GetAllCategoriesQuery, Ardalis.Result.Result<IEnumerable<CategoryQueryResult>>>,
    IQueryHandler<GetCategoryByIdQuery, Ardalis.Result.Result<CategoryQueryResult>>

{
    private readonly RiseFinancialDbContext _dbContext;

    public CategoryQueryHandler(RiseFinancialDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Ardalis.Result.Result<IEnumerable<CategoryQueryResult>>> Handle(
        GetAllCategoriesQuery query,
        CancellationToken cancellationToken)
        => Ardalis.Result.Result<IEnumerable<CategoryQueryResult>>
            .Success(await _dbContext.Categories
            .AsNoTracking()
            .Select(x => new CategoryQueryResult(x.Id, x.Name))
            .ToListAsync(cancellationToken));

    public async ValueTask<Ardalis.Result.Result<CategoryQueryResult>> Handle(
        GetCategoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new CategoryQueryResult(x.Id, x.Name))
            .FirstOrDefaultAsync(cancellationToken);
        
        return category is null ?
            Ardalis.Result.Result<CategoryQueryResult>.NotFound() : 
            Ardalis.Result.Result<CategoryQueryResult>.Success(category);
    }
}