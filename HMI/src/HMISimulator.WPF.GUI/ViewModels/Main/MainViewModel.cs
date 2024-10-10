namespace HMISimulator.WPF.GUI.ViewModels.Main;

public sealed class MainViewModel : BaseViewModel, IMainViewModel
{
    private double _heatCapacity;
    private double _heatLossCoefficient;
    private double _heaterPowerPercentage;
    private double _ambientTemperature;
    private double _targetTemperature;

    public double HeatCapacity
    {
        get => _heatCapacity;
        set => SetProperty(ref _heatCapacity, value);
    }

    public double HeatLossCoefficient
    {
        get => _heatLossCoefficient;
        set => SetProperty(ref _heatLossCoefficient, value);
    }

    public double HeaterPowerPercentage
    {
        get => _heaterPowerPercentage;
        set => SetProperty(ref _heaterPowerPercentage, value);
    }

    public double AmbientTemperature
    {
        get => _ambientTemperature;
        set => SetProperty(ref _ambientTemperature, value);
    }

    public double TargetTemperature
    {
        get => _targetTemperature;
        set => SetProperty(ref _targetTemperature, value);
    }

    public MainViewModel()
    {
        HeatCapacity = 5000.0;
        HeatLossCoefficient = 0.1;
        HeaterPowerPercentage = 100.0;
        AmbientTemperature = 19.0;
        TargetTemperature = 300.0;
    }
}