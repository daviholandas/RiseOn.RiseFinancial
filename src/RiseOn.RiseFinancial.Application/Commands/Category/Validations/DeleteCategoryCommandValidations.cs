using FluentValidation;

namespace RiseOn.RiseFinancial.Application.Commands.Category.Validations;

public class DeleteCategoryByIdCommandValidation : AbstractValidator<DeleteCategoryByIdCommand>
{
    public DeleteCategoryByIdCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class DeleteCategoryByNameCommandValidation : AbstractValidator<DeleteCategoryByNameCommand>
{
    public DeleteCategoryByNameCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}