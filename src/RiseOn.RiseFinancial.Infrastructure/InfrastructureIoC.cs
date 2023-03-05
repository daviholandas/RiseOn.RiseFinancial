using Microsoft.Extensions.DependencyInjection;
using RiseOn.RiseFinancial.Infrastructure.Data;

namespace RiseOn.RiseFinancial.Infrastructure;

public static class InfrastructureIoC
{
    public static IServiceCollection AddInfrastructureDataServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSqlServer<RiseFinancialDbContext>("", options =>
        {
            options.EnableRetryOnFailure(3);
        });
        
        return serviceCollection;
    }
}