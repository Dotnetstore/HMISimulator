namespace HMISimulator.API.SDK.Oven.Requests;

public record struct AddRecipeRequest(
    double HeatCapacity,
    double HeatLossCoefficient,
    double HeaterPowerPercentage,
    double AmbientTemperature,
    double TargetTemperature);