using FastEndpoints;
using HMISimulator.API.Oven.Health;
using HMISimulator.API.Oven.Ovens;
using HMISimulator.API.Oven.Recipes;
using HMISimulator.API.Oven.Services;
using HMISimulator.API.SharedKernel.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HMISimulator.API.Oven.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOven(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

        services
            .AddFastEndpoints()
            .AddHostedService<OvenSimulatorService>()
            .AddDbContext<OvenDataContext>(connectionString)
            .AddSingleton<IOvenSimulator, OvenSimulator>()
            .AddScoped<IOvenService, OvenService>()
            .AddScoped<IRecipeRepository, RecipeRepository>()
            .AddScoped<IRecipeService, RecipeService>()
            .AddScoped<IOvenUnitOfWork, OvenUnitOfWork>()
            .EnsureDbCreated<OvenDataContext>();

        services
            .AddHealthChecks()
            .AddSqlServer(connectionString, healthQuery: "SELECT 1", name: "SQL Server",
                failureStatus: HealthStatus.Unhealthy)
            .AddCheck<DatabaseHealthCheck>("Database oven")
            .AddCheck<MemoryHealthCheck>("Memory Database oven");
        
        services
            .AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);
                opt.AddHealthCheckEndpoint("Oven", "/health/oven");
            })
            .AddInMemoryStorage();
        
        return services;
    }
}