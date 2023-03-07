using Ardalis.Result;
using Mediator;
using RiseOn.RiseFinancial.Infrastructure.Data;

namespace RiseOn.RiseFinancial.Application.Commands.Category;

public record struct CreateCategoryCommand(string Name)
    : ICommand<Result<Guid>>;

public class CreateCategoryCommandHandler 
    : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly RiseFinancialDbContext _dbContext;

    public CreateCategoryCommandHandler(RiseFinancialDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result<Guid>> Handle(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var addResult = await _dbContext.Categories
            .AddAsync(new(command.Name), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(addResult.Entity.Id);
    }
}
