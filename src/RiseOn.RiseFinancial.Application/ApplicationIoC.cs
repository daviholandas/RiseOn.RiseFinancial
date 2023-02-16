using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RiseOn.RiseFinancial.Application;

public static class ApplicationIoC
{
    public static IServiceCollection AddApplicationIoC(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediator(config =>
        {
            config.Namespace = "RiseOn.RiseFinancial";
            config.ServiceLifetime = ServiceLifetime.Transient;
        });

        return serviceCollection;
    }
}