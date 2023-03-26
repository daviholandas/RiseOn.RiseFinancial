using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using RiseOn.RiseFinancial.Application.Commands.Category;
using RiseOn.RiseFinancial.Infra.Data.Queries.Category;
using IResult = Ardalis.Result.IResult;

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

        group.MapGet("{id:guid}", async (Guid id, IMediator mediator)
                => await mediator.Send(new GetCategoryByIdQuery(id))
                    switch
                    {
                        { IsSuccess: true } result => Results.Ok(result.Value),
                        { Status: ResultStatus.NotFound } => Results.NotFound(),
                        _ => Results.BadRequest()
                    })
            .WithName("GetCategoryById")
            .Produces(StatusCodes.Status200OK, typeof(CategorySwaggerModel))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async (IMediator mediator) 
                => await mediator.Send(new GetAllCategoriesQuery())
                        switch
                        {
                            { IsSuccess: true } result => Results.Ok(result.Value),
                            _ => Results.BadRequest()
                        })
            .Produces(StatusCodes.Status200OK, typeof(CategorySwaggerModel));

        group.MapPut("{id:guid}", async ([FromRoute] Guid id, [FromBody] string newName, IMediator mediator)
            => await mediator.Send(new UpdateCategoryByIdCommand(id, newName))
                switch
                {
                    { IsSuccess: true } result => Results.NoContent(),
                    { Status: ResultStatus.NotFound } => Results.NotFound(),
                    _ => Results.BadRequest()
                })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapDelete("{id:guid}", async ([FromRoute] Guid id, IMediator mediator)
                => await mediator.Send(new DeleteCategoryByIdCommand(id))
                    switch
                    {
                        { IsSuccess: true } => Results.Accepted(),
                        { Status: ResultStatus.NotFound } => Results.NoContent(),
                        _ => Results.BadRequest()
                    })
            .Produces(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);
        
        return routeBuilder;
    }
}

internal record struct CategorySwaggerModel(Guid Id, string Name);