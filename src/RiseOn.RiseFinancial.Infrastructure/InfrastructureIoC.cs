using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiseOn.RiseFinancial.CrossCutting.Models;
using RiseOn.RiseFinancial.Infrastructure.Data;

namespace RiseOn.RiseFinancial.Infrastructure;

public static class InfrastructureIoC
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

        return serviceCollection;
    }
}