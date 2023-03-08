using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Infrastructure.Data;

namespace RiseOn.RiseFinancial.Application.Commands.Category.Validations;

public class CreateCategoryCommandValidations : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidations(RiseFinancialDbContext dbContext)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (property, cancellationToken)
                => !await dbContext.Categories
                    .AsNoTracking()
                    .AnyAsync(x => String.Equals(x.Name.ToUpper(), property.ToUpper()), cancellationToken))
            .WithMessage("Already exist a category with the same name registered.")
            .NotEmpty();
    }
}