using System.Linq.Expressions;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Infra.Data;
using RiseOn.RiseFinancial.Infra.Data.Data;

namespace RiseOn.RiseFinancial.Application.Commands.Category;

public record struct DeleteCategoryByIdCommand(Guid Id) : 
    ICommand<Result>;

public record struct DeleteCategoryByNameCommand(string Name) :
    ICommand<Result>;

public class DeleteCategoryCommandHandlers :
    ICommandHandler<DeleteCategoryByIdCommand, Result>,
    ICommandHandler<DeleteCategoryByNameCommand, Result>
{
    private readonly RiseFinancialDbContext _dbContext;

    public DeleteCategoryCommandHandlers(RiseFinancialDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result> Handle(
        DeleteCategoryByIdCommand command,
        CancellationToken cancellationToken)
        => await DeleteCategoryAsync(
            c => c.Id == command.Id,
            cancellationToken);

    public async ValueTask<Result> Handle(
        DeleteCategoryByNameCommand command,
        CancellationToken cancellationToken)
        => await DeleteCategoryAsync(
            c => c.Name.ToUpper().Equals(command.Name.ToUpper()),
            cancellationToken);

    private async ValueTask<Result> DeleteCategoryAsync(
        Expression<Func<Core.ExpenseAggregate.Category, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var entryResult = await _dbContext.Categories
            .Where(predicate)
            .ExecuteDeleteAsync(cancellationToken);

        return entryResult > 0
            ? Result.Success()
            : Result.NotFound();
    }
}
    