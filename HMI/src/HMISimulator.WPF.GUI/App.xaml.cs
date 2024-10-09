using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using HMISimulator.WPF.GUI.Extensions;
using HMISimulator.WPF.GUI.ViewModels.Main;
using HMISimulator.WPF.GUI.Views.Main;
using Microsoft.Extensions.DependencyInjection;

namespace HMISimulator.WPF.GUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private IServiceCollection _serviceCollection = null!;

    private void AppOnStartup(object sender, StartupEventArgs e)
    {
        ConfigureServices();
        SetCulture("sv-SE");
        StartApplication();
    }
    
    private void ConfigureServices()
    {
        _serviceCollection = new ServiceCollection();
        
        _serviceCollection
            .AddGUI();
    }

    private void SetCulture(string culture)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
            XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
    }
    
    private void StartApplication()
    {
        using var scope = _serviceCollection.BuildServiceProvider().CreateScope();
        var viewModel = scope.ServiceProvider.GetRequiredService<IMainViewModel>();
        var mainWindow = new MainView { DataContext = viewModel };
        mainWindow.Show();
    }
}