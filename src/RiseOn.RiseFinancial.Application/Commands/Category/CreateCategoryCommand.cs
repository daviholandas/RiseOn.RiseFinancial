using Ardalis.Result;
using MapsterMapper;
using Mediator;
using RiseOn.RiseFinancial.Infra.Data;
using RiseOn.RiseFinancial.Infra.Data.Data;

namespace RiseOn.RiseFinancial.Application.Commands.Category;

public record struct CreateCategoryCommand(string Name)
    : ICommand<Result<Guid>>;

public class CreateCategoryCommandHandler 
    : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly RiseFinancialDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(
        RiseFinancialDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<Guid>> Handle(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var addResult = await _dbContext.Categories
            .AddAsync(_mapper.Map<Core.ExpenseAggregate.Category>(command), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(addResult.Entity.Id);
    }
}
