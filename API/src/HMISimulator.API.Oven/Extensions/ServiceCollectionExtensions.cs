using FastEndpoints;
using HMISimulator.API.Oven.Recipes;
using HMISimulator.API.SharedKernel.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HMISimulator.API.Oven.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOven(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var directory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\HMISimulator";
        var connectionString = $"{directory}\\{configuration.GetValue<string>("ConnectionStrings:DefaultConnection")}";
        
        CreateApplicationFolder(directory);
        
        services
            .AddFastEndpoints()
            .AddHostedService<OvenSimulatorService>()
            .AddDbContext<OvenDataContext>($"Data Source={connectionString}")
            .AddSingleton<IOvenSimulator, OvenSimulator>()
            .AddScoped<IRecipeRepository, RecipeRepository>()
            .AddScoped<IRecipeService, RecipeService>()
            .AddScoped<IOvenUnitOfWork, OvenUnitOfWork>()
            .EnsureDbCreated<OvenDataContext>();
        
        return services;
    }

    private static void CreateApplicationFolder(string directory)
    {
        if(!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}