using System.Linq.Expressions;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Infra.Data;
using RiseOn.RiseFinancial.Infra.Data.Data;

namespace RiseOn.RiseFinancial.Application.Commands.Category;

public record struct UpdateCategoryByIdCommand(Guid Id, string Name) :
    ICommand<Result>;
    
public record struct UpdateCategoryByNameCommand(string Name, string NewName) :
    ICommand<Result>;


public class UpdateCategoryCommandHandlers :
    ICommandHandler<UpdateCategoryByIdCommand, Result>,
    ICommandHandler<UpdateCategoryByNameCommand, Result>
{
    private readonly RiseFinancialDbContext _dbContext;

    public UpdateCategoryCommandHandlers(RiseFinancialDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result> Handle(
        UpdateCategoryByIdCommand command,
        CancellationToken cancellationToken)
        => await UpdateCategoryAsync(
            x => x.Id == command.Id,
            command.Name,
            cancellationToken);

    public async ValueTask<Result> Handle(UpdateCategoryByNameCommand command, CancellationToken cancellationToken)
        => await UpdateCategoryAsync(
            x => x.Name.ToUpper().Equals(command.Name.ToUpper()),
            command.NewName,
            cancellationToken);

    private async ValueTask<Result> UpdateCategoryAsync(
        Expression<Func<Core.ExpenseAggregate.Category, bool>> predicate,
        string name,
        CancellationToken cancellationToken)
    {
        var result = await _dbContext.Categories
            .Where(predicate)
            .ExecuteUpdateAsync(c 
                => c.SetProperty(x => x.Name,
                    x => name), cancellationToken);

        return result > 0
            ? Result.Success()
            : Result.NotFound();
    }
}