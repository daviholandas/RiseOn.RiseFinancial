using FluentValidation;

namespace RiseOn.RiseFinancial.Application.Commands.Expense.Validations;

public class CreateFixedExpenseCommandValidation : AbstractValidator<CreateFixedExpenseCommand>
{
    public CreateFixedExpenseCommandValidation()
    {
        RuleFor(x => x.Value)
            .GreaterThan(decimal.Zero);
        
        RuleFor(x => x.WalletId)
            .NotEmpty()
            .WithMessage("{PropertyName} can't be empty or null.");
    }
}

public class CreateVariableExpenseCommandValidation : AbstractValidator<CreateVariableExpenseCommand>
{
    public CreateVariableExpenseCommandValidation()
    {
        RuleFor(x => x.Value)
            .GreaterThan(decimal.Zero);
        
        RuleFor(x => x.WalletId)
            .NotEmpty()
            .WithMessage("{PropertyName} can't be empty or null.");
    }
}