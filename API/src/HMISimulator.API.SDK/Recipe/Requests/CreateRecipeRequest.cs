namespace HMISimulator.API.SDK.Recipe.Requests;

public record struct CreateRecipeRequest(
    string Name,
    double HeatCapacity,
    double HeatLossCoefficient,
    double HeaterPowerPercentage,
    double AmbientTemperature,
    double TargetTemperature);