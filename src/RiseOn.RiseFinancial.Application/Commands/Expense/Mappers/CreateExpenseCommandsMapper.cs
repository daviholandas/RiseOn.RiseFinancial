using Mapster;

namespace RiseOn.RiseFinancial.Application.Commands.Expense.Mappers;

public class CreateExpenseCommandsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        var ignoreProperties = new string[]
        {
            nameof(Core.ExpenseAggregate.Expense.ExpenseType),
            nameof(Core.ExpenseAggregate.Expense.Category),
            nameof(Core.ExpenseAggregate.Expense.Status),
            nameof(Core.ExpenseAggregate.Expense.Id),
            nameof(Core.ExpenseAggregate.Expense.CreateAt),
            nameof(Core.ExpenseAggregate.Expense.IsActive),
        };
        
        config.When(((srcType, destType, mapType) =>
                destType == typeof(Core.ExpenseAggregate.Expense)))
            .Ignore(ignoreProperties);

        config.NewConfig<CreateVariableExpenseCommand, Core.ExpenseAggregate.Expense>()
            .EnumMappingStrategy(EnumMappingStrategy.ByName)
            .MapToConstructor(true)
            .Ignore(x => x.InstallmentNumber, y => y.DueDay)
            .ConstructUsing(src => new(
                src.Value, src.Description,
                src.Recipient, src.CategoryId,
                src.WalletId));

        config.NewConfig<CreateFixedExpenseCommand, Core.ExpenseAggregate.Expense>()
            .EnumMappingStrategy(EnumMappingStrategy.ByName)
            .MapToConstructor(true)
            .ConstructUsing(src => new(
                src.Value, src.Description,
                src.Recipient, src.CategoryId,
                src.WalletId, src.InstallmentNumber,
                src.DueDay));
    }
}