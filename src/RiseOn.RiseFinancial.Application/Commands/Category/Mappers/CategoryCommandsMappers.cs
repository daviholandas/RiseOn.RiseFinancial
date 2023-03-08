using Mapster;

namespace RiseOn.RiseFinancial.Application.Commands.Category.Mappers;

public class CategoryCommandsMappers : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryCommand, Core.ExpenseAggregate.Category>()
            .MapToConstructor(true)
            .ConstructUsing(src 
                => new(src.Name));
    }
}