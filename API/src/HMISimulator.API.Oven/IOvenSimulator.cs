using HMISimulator.API.SDK;

namespace HMISimulator.API.Oven;

internal interface IOvenSimulator
{
    public double CurrentTemperature { get; internal set; }
    public double AmbientTemperature { get; internal set; }
    public double TargetTemperature { get; internal set; }
    public double HeatCapacity { get; internal set; }
    public double HeatLossCoefficient { get; internal set; }
    public double HeaterPowerPercentage { get; internal set; }
    public bool HeatingElementOn { get; internal set; }
    
    void SetHeaterPower(double percentage);
    
    void StartHeating();
    
    void StopHeating();
    
    void Update(double deltaTime);
    
    void RungeKuttaStep(double deltaTime);
    
    void SimulateError(OvenErrorType error);
}