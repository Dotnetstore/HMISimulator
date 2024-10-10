using HMISimulator.API.SharedKernel.Models;

namespace HMISimulator.API.Oven.Recipes;

public sealed class Recipe : BaseAuditableEntity
{
    public required RecipeId Id { get; init; }

    public required string Name { get; init; }

    public required double HeatCapacity { get; init; }

    public required double HeatLossCoefficient { get; init; }

    public required double HeaterPowerPercentage { get; init; }

    public required double AmbientTemperature { get; init; }
    
    public required double TargetTemperature { get; init; }
}

public record struct RecipeId(Guid Value);