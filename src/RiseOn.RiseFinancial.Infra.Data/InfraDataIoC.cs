using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiseOn.RiseFinancial.CrossCutting.Models;
using RiseOn.RiseFinancial.Infra.Data.Graphql.Queries;

namespace RiseOn.RiseFinancial.Infra.Data;

public static class InfraDataIoC
{
    public static IServiceCollection AddInfrastructureDataServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var applicationSettings = configuration
            .GetSection(nameof(ApplicationSettings))
            .Get<ApplicationSettings>();

        serviceCollection.AddSqlServer<RiseFinancialDbContext>(
            applicationSettings!.Database!.ConnectionUrl, 
            options => options.EnableRetryOnFailure(3), 
            optionBuilder => optionBuilder.EnableSensitiveDataLogging(true));

        serviceCollection
            .AddGraphQLServer()
            .AddQueryType<CategoryQuery>()
            .RegisterDbContext<RiseFinancialDbContext>()
            .AddDataTypes();

        return serviceCollection;
    }
}