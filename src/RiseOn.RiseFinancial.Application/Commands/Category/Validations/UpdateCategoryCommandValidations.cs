using FluentValidation;

namespace RiseOn.RiseFinancial.Application.Commands.Category.Validations;

public class UpdateCategoryByIdCommandValidation : AbstractValidator<UpdateCategoryByIdCommand>
{
    public UpdateCategoryByIdCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}

public class UpdateCategoryByNameCommandValidation : AbstractValidator<UpdateCategoryByNameCommand>
{
    public UpdateCategoryByNameCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.NewName)
            .NotEmpty();
    }
}