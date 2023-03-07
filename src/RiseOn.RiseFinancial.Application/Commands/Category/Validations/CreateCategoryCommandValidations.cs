using FluentValidation;

namespace RiseOn.RiseFinancial.Application.Commands.Category.Validations;

public class CreateCategoryCommandValidations : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidations()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}