using FluentValidation;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiseOn.RiseFinancial.Application.Configurations.BehaviourPipelines;
using RiseOn.RiseFinancial.Application.Models;

namespace RiseOn.RiseFinancial.Application;

public static class ApplicationIoC
{
    public static IServiceCollection AddApplicationIoC(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        serviceCollection
            .Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)));

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