using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Infra.Data;
using RiseOn.RiseFinancial.Infra.Data.Data;

namespace RiseOn.RiseFinancial.Application.Extensions.Validators;

public static class CustomValidatorsExtensions
{
    public static IRuleBuilderOptions<T, Guid> ValidCategoryByIdAsync<T, Guid>(
        this IRuleBuilder<T, Guid> ruleBuilderOptions,
        RiseFinancialDbContext financialDbContext)
            => ruleBuilderOptions
                .MustAsync(async (id, cancellationToken)
                    => await financialDbContext.Categories.AnyAsync(x => x.Id.Equals(id), cancellationToken))
                .WithMessage("The category doesn't exist!");
}