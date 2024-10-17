using HMISimulator.API.Contracts.Recipes;
using HMISimulator.API.SDK;

namespace HMISimulator.API.Oven.Ovens;

internal interface IOvenSimulator
{
    public Recipe? ActiveRecipe { get; internal set; }
    
    public bool HeatingElementOn { get; internal set; }
    
    public double CurrentTemperature { get; internal set; }
    
    public OvenErrorType CurrentError { get; internal set; }
    
    void StartHeating();
    
    void StopHeating();
    
    void Update(double deltaTime);
    
    void RungeKuttaStep(double deltaTime);
    
    void SimulateError(OvenErrorType error);
}