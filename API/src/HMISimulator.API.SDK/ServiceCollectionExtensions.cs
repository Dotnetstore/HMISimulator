using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace HMISimulator.API.SDK;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHmiSimulatorApiSdk(
        this IServiceCollection services,
        string baseAddress)
    {
        const string authToken = "This is the access bearer token. It Should be complex and long. This is valid for HMISimulator API.";
        
        var refitSettings = new RefitSettings
        {
            AuthorizationHeaderValueGetter = (rq, ct) => Task.FromResult(authToken)
        };
        
        services
            .AddRefitClient<IOvenService>(refitSettings)
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));

        return services;
    }
}