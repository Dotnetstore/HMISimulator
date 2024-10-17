using System.Reflection;
using HMISimulator.API.Amazon.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HMISimulator.API.Amazon.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazon(this IServiceCollection services, List<Assembly> mediatRAssemblies)
    {
        mediatRAssemblies.Add(typeof(IAmazonAssemblyMarker).Assembly);
        
        services
            .AddScoped<IAmazonSqsService, AmazonSqsService>();

        return services;
    }
}