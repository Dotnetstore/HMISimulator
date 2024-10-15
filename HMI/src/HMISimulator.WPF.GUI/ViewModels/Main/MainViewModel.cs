using CommunityToolkit.Mvvm.Input;
using HMISimulator.WPF.GUI.Services;
using HMISimulator.WPF.GUI.ViewModels.Oven;

namespace HMISimulator.WPF.GUI.ViewModels.Main;

internal sealed class MainViewModel : BaseViewModel, IMainViewModel, IDisposable
{
    private readonly INavigationService _navigationService;
    private readonly IOvenControlViewModel _ovenControlViewModel;
    private readonly IRecipeViewModel _recipeViewModel;
    private IBaseViewModel _mainContent = null!;

    public IBaseViewModel MainContent
    {
        get => _mainContent;
        set => SetProperty(ref _mainContent, value);
    }

    public RelayCommand OpenOvenControlCommand { get; set; }
    public RelayCommand OpenRecipeCommand { get; set; }

    public MainViewModel(
        INavigationService navigationService,
        IOvenControlViewModel ovenControlViewModel,
        IRecipeViewModel recipeViewModel)
    {
        _navigationService = navigationService;
        _ovenControlViewModel = ovenControlViewModel;
        _recipeViewModel = recipeViewModel;
        
        OpenOvenControlCommand = new RelayCommand(ExecuteOpenOvenControl);
        OpenRecipeCommand = new RelayCommand(ExecuteOpenRecipe);

        _navigationService.NavigateToOvenControl += NavigateToOvenControl;
        _navigationService.NavigateToRecipe += NavigateToRecipe;
    }

    private void ExecuteOpenOvenControl()
    {
        _navigationService.OnNavigateToOvenControl();
    }

    private void ExecuteOpenRecipe()
    {
        _navigationService.OnNavigateToRecipe();
    }

    void IMainViewModel.Load()
    {
        _navigationService.OnNavigateToOvenControl();
    }

    private void NavigateToOvenControl(object? sender, EventArgs e)
    {
        SetMainContent(_ovenControlViewModel);
    }

    private void NavigateToRecipe(object? sender, EventArgs e)
    {
        Task.Run(() => _recipeViewModel.LoadAsync()).Wait();
        SetMainContent(_recipeViewModel);
    }

    private void SetMainContent(IBaseViewModel viewModel)
    {
        MainContent = viewModel;
    }
    
    public void Dispose()
    {
        _navigationService.NavigateToOvenControl -= NavigateToOvenControl;
        _navigationService.NavigateToRecipe -= NavigateToRecipe;
    }
    // private double _heatCapacity;
    // private double _heatLossCoefficient;
    // private double _heaterPowerPercentage;
    // private double _ambientTemperature;
    // private double _targetTemperature;
    //
    // public double HeatCapacity
    // {
    //     get => _heatCapacity;
    //     set => SetProperty(ref _heatCapacity, value);
    // }
    //
    // public double HeatLossCoefficient
    // {
    //     get => _heatLossCoefficient;
    //     set => SetProperty(ref _heatLossCoefficient, value);
    // }
    //
    // public double HeaterPowerPercentage
    // {
    //     get => _heaterPowerPercentage;
    //     set => SetProperty(ref _heaterPowerPercentage, value);
    // }
    //
    // public double AmbientTemperature
    // {
    //     get => _ambientTemperature;
    //     set => SetProperty(ref _ambientTemperature, value);
    // }
    //
    // public double TargetTemperature
    // {
    //     get => _targetTemperature;
    //     set => SetProperty(ref _targetTemperature, value);
    // }
    //
    // public MainViewModel()
    // {
    //     HeatCapacity = 5000.0;
    //     HeatLossCoefficient = 0.1;
    //     HeaterPowerPercentage = 100.0;
    //     AmbientTemperature = 19.0;
    //     TargetTemperature = 300.0;
    // }
}