using Ardalis.Result;
using Mediator;
using RiseOn.RiseFinancial.Application.Commands.Category;
using RiseOn.RiseFinancial.Infrastructure.Data.Queries.Category;

namespace RiseOn.RiseFinancial.WebApi.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder AddCategoryEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder
            .MapGroup("category")
            .WithTags("Category")
            .WithOpenApi();

        Delegate handler = async (ICommand command, IMediator mediator)
            => await mediator.Send(command) switch
            {
                {}
                _ => Results.BadRequest()
            };

        group.MapPost("", async (IMediator mediator, CreateCategoryCommand command)
                => await mediator.Send(command)
                    switch
                    {
                        { IsSuccess: true } result => Results
                            .CreatedAtRoute("GetCategoryById",new { id = result.Value }, result.Value),
                        var result => Results.BadRequest(result.Errors)
                    })
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("{id:guid}", async (Guid id, IMediator mediator)
                => await mediator.Send(new GetCategoryByIdQuery(id))
                    switch
                    {
                        { IsSuccess: true } result => Results.Ok(result.Value),
                        { Status: ResultStatus.NotFound } => Results.NotFound(),
                        _ => Results.BadRequest()
                    })
            .WithName("GetCategoryById")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async (IMediator mediator) 
                => (await mediator.Send(new GetAllCategoriesQuery())).Value)
            .Produces(StatusCodes.Status200OK);

        group.MapPut("", async (UpdateCategoryByIdCommand command, IMediator mediator)
            => await mediator.Send(command)
                switch
                {
                    { IsSuccess: true } result => Results.Accepted(),
                    { Status: ResultStatus.NotFound } => Results.NotFound(),
                    _ => Results.BadRequest()
                });
        return routeBuilder;
    }
}