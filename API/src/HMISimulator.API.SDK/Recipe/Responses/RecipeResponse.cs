namespace HMISimulator.API.SDK.Recipe.Responses;

public record struct RecipeResponse(
    Guid Id,
    string Name,
    double HeatCapacity,
    double HeatLossCoefficient,
    double HeaterPowerPercentage,
    double AmbientTemperature,
    double TargetTemperature);