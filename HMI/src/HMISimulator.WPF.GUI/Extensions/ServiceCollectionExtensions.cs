using HMISimulator.WPF.GUI.ViewModels.Main;
using HMISimulator.WPF.GUI.Views.Main;
using Microsoft.Extensions.DependencyInjection;

namespace HMISimulator.WPF.GUI.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGUI(this IServiceCollection services)
    {
        services
            .AddSingleton<IMainViewModel, MainViewModel>()
            .AddTransient<MainView>();
        
        return services;
    }
}