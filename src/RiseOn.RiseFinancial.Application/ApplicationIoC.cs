using System.Reflection;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using RiseOn.RiseFinancial.Application.Configurations.BehaviourPipelines;

namespace RiseOn.RiseFinancial.Application;

public static class ApplicationIoC
{
    public static IServiceCollection AddApplicationIoC(this IServiceCollection serviceCollection)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        serviceCollection
            .AddMediator(config =>
                config.ServiceLifetime = ServiceLifetime.Transient)
            .AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehaviour<,>));

        serviceCollection
            .AddValidatorsFromAssemblies(assemblies);

        return serviceCollection;
    }
}