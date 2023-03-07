using FluentValidation;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiseOn.RiseFinancial.Application.Configurations.BehaviourPipelines;
using RiseOn.RiseFinancial.CrossCutting.Models;

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
            .AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehaviour<,>));

        serviceCollection
            .AddValidatorsFromAssemblies(assemblies);

        return serviceCollection;
    }
}