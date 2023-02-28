using Mediator;
using RiseOn.RiseFinancial.Application.Commands.Expense;

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

        group.MapPost("fixed", 
                async (IMediator mediator, CreateFixedExpenseCommand command)
                    => await mediator.Send(command)
                switch 
                {
                    {IsSuccess: true} result =>  Results.Created($"{result.Value}", result.Value),
                    var result => Results.BadRequest(result.Errors)
                })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("variable", 
                async (IMediator mediator, CreateVariableExpenseCommand command)
                    => await mediator.Send(command)
                        switch 
                        {
                            { IsSuccess: true } result =>  Results.Created($"{result.Value}", result.Value),
                            var result => Results.BadRequest(result.Errors)
                        })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        return builder;
    }
}