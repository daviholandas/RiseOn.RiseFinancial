using Mediator;
using RiseOn.RiseFinancial.Application.UseCases.Expense;

namespace RiseOn.RiseFinancial.WebApi.Endpoints;

public static class ExpenseEndpoints
{
    public static IEndpointRouteBuilder AddExpenseEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("expense")
            .WithDisplayName("Expense")
            .WithTags("Expense")
            .WithOpenApi();

        group.MapPost("fixed", async (IMediator mediator, CreateFixedExpenseCommand command) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"{result.Value}", result.Value);
        })
            .Produces(StatusCodes.Status201Created);

        return builder;
    }
}