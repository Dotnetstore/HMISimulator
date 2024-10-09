using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace HMISimulator.API.Oven;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOven(this IServiceCollection services)
    {
        services
            .AddFastEndpoints()
            .AddHostedService<OvenSimulatorService>()
            .AddSingleton<IOvenSimulator, OvenSimulator>();
        
        return services;
    }
}