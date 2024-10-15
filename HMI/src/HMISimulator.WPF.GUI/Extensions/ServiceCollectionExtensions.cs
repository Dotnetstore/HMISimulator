using HMISimulator.WPF.GUI.Services;
using HMISimulator.WPF.GUI.ViewModels.Main;
using HMISimulator.WPF.GUI.ViewModels.Oven;
using HMISimulator.WPF.GUI.Views.Main;
using HMISimulator.WPF.GUI.Views.Oven;
using Microsoft.Extensions.DependencyInjection;

namespace HMISimulator.WPF.GUI.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGUI(this IServiceCollection services)
    {
        services
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<IMainViewModel, MainViewModel>()
            .AddTransient<MainView>()
            .AddSingleton<IOvenControlViewModel, OvenControlViewModel>()
            .AddTransient<OvenControlView>()
            .AddSingleton<IRecipeViewModel, RecipeViewModel>()
            .AddTransient<RecipeView>();
        
        return services;
    }
}