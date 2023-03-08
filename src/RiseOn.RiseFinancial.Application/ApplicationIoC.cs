using FluentValidation;
using Mapster;
using MapsterMapper;
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
        
        //Mediator
        serviceCollection
            .AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehaviour<,>));
       
        //FluentValidation
        serviceCollection
            .AddValidatorsFromAssemblies(assemblies);
        
        //Mappers
        var mapsterConfig = new TypeAdapterConfig
        {
            AllowImplicitDestinationInheritance = true,
            RequireExplicitMapping = true,
            RequireDestinationMemberSource = true,
            Compiler = exp => exp.Compile()
        };
        mapsterConfig.Scan(assemblies);
        serviceCollection
            .AddSingleton(mapsterConfig)
            .AddTransient<IMapper, ServiceMapper>();

        return serviceCollection;
    }
}