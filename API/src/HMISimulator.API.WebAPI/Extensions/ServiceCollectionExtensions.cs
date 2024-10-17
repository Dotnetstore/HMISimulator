using System.Reflection;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using HMISimulator.API.Amazon;
using HMISimulator.API.Amazon.Extensions;
using HMISimulator.API.Contracts;
using HMISimulator.API.Oven.Extensions;
using HMISimulator.API.SharedKernel.Behavior;
using HMISimulator.API.SharedKernel.Extensions;
using MediatR;

namespace HMISimulator.API.WebAPI.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddWebApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        List<Assembly> mediatRAssemblies =
        [
            typeof(Program).Assembly,
            typeof(IContractsAssemblyMarker).Assembly

        ];

        services
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddSharedKernel(mediatRAssemblies)
            .AddAmazon(mediatRAssemblies)
            .AddOven(configuration, mediatRAssemblies)
            .AddMediatR(x => x.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()))
            .AddHealthChecks();
        
        services
            .AddValidatorsFromAssemblyContaining<IAmazonAssemblyMarker>()
            .AddMediatRLoggingBehavior()
            .AddMediatRFluentValidationBehavior();
        
        return services;
    }

    private static IServiceCollection AddValidatorsFromAssemblyContaining<T>(
        this IServiceCollection services)
    {
        // Get the assembly containing the specified type
        var assembly = typeof(T).GetTypeInfo().Assembly;

        // Find all validator types in the assembly
        var validatorTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && 
                          i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .ToList();

        // Register each validator with its implemented interfaces
        foreach (var validatorType in validatorTypes)
        {
            var implementedInterfaces = validatorType.GetInterfaces()
                .Where(i => i.IsGenericType && 
                            i.GetGenericTypeDefinition() == typeof(IValidator<>));

            foreach (var implementedInterface in implementedInterfaces)
            {
                services.AddTransient(implementedInterface, validatorType);
            }
        }

        return services;
    }

    private static IServiceCollection AddMediatRLoggingBehavior(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), 
            typeof(LoggingBehavior<,>));
        return services;
    }
    
    private static IServiceCollection AddMediatRFluentValidationBehavior(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>),
            typeof(FluentValidationBehavior<,>));

        return services;
    }
}