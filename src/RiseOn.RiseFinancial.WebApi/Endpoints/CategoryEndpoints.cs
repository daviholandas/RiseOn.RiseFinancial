using Mediator;
using RiseOn.RiseFinancial.Application.Commands.Category;

namespace RiseOn.RiseFinancial.WebApi.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder AddCategoryEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder
            .MapGroup("category")
            .WithTags("Category")
            .WithOpenApi();

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

        group.MapGet("{id:guid}", () => Results.Ok())
            .WithName("GetCategoryById");

        return routeBuilder;
    }
}