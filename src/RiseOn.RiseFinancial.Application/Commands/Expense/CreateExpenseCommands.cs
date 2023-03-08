using Ardalis.Result;
using MapsterMapper;
using Mediator;
using RiseOn.RiseFinancial.Infrastructure.Data;

namespace RiseOn.RiseFinancial.Application.Commands.Expense;

public record struct CreateFixedExpenseCommand(
        decimal Value, string? Description,
        string Recipient, Guid CategoryId,
        Guid WalletId, int InstallmentNumber,
        int? DueDay)
    : ICommand<Result<Guid>>;

public record struct CreateVariableExpenseCommand(
        decimal Value, string? Description,
        string Recipient, Guid CategoryId,
        Guid WalletId)
    : ICommand<Result<Guid>>;


public class CreateExpensesHandler : 
    ICommandHandler<CreateFixedExpenseCommand, Result<Guid>>, 
    ICommandHandler<CreateVariableExpenseCommand, Result<Guid>>
{
    private readonly RiseFinancialDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateExpensesHandler(
        RiseFinancialDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<Guid>> Handle(CreateFixedExpenseCommand command,
        CancellationToken cancellationToken)
        => await SaveAsync(command, cancellationToken);
    
    public async ValueTask<Result<Guid>> Handle(CreateVariableExpenseCommand command,
        CancellationToken cancellationToken)
        => await SaveAsync(command, cancellationToken);

    private async ValueTask<Result<Guid>> SaveAsync<T>(ICommand<T> command,
        CancellationToken cancellationToken)
    {
        var entityEntry = await _dbContext.Expenses
            .AddAsync(
                _mapper.Map<Core.ExpenseAggregate.Expense>(command),
                cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(entityEntry.Entity.Id);
    }
}
