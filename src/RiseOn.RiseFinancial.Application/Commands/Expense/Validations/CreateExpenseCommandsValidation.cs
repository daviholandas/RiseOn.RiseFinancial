using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Application.Extensions.Validators;
using RiseOn.RiseFinancial.Infra.Data;
using RiseOn.RiseFinancial.Infra.Data.Data;

namespace RiseOn.RiseFinancial.Application.Commands.Expense.Validations;

public class CreateFixedExpenseCommandValidation : AbstractValidator<CreateFixedExpenseCommand>
{
    public CreateFixedExpenseCommandValidation(RiseFinancialDbContext riseFinancialDbContext)
    {
        RuleFor(x => x.Value)
            .GreaterThan(decimal.Zero);

        RuleFor(x => x.WalletId)
            .NotEmpty()
            .WithMessage("{PropertyName} can't be empty or null.");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("{PropertyName} can't be empty or null.")
            .ValidCategoryByIdAsync(riseFinancialDbContext);

        RuleFor(x => x.InstallmentNumber)
            .GreaterThan(0);

        RuleFor(x => x.DueDay)
            .Must(day => day is >= 1 and <= 31)
            .WithMessage("{PropertyName} need be greater or equal to date today.");
    }
}

public class CreateVariableExpenseCommandValidation : AbstractValidator<CreateVariableExpenseCommand>
{
    public CreateVariableExpenseCommandValidation(RiseFinancialDbContext riseFinancialDbContext)
    {
        RuleFor(x => x.Value)
            .GreaterThan(decimal.Zero);
        
        RuleFor(x => x.WalletId)
            .NotEmpty()
            .WithMessage("{PropertyName} can't be empty or null.");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("{PropertyName} can't be empty or null.")
            .ValidCategoryByIdAsync(riseFinancialDbContext);
    }
}