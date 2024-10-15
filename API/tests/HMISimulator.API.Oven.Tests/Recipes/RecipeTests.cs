using FluentAssertions;
using FluentAssertions.Execution;
using HMISimulator.API.Oven.Recipes;
using Xunit;

namespace HMISimulator.API.Oven.Tests.Recipes;

public class RecipeTests
{
    [Fact]
    public void Recipe_Properties_Should_Have_Valid_Values()
    {
        // Arrange
        var id = new RecipeId(Guid.NewGuid());
        const string name = "Pizza";
        const double heatCapacity = 5000.0;
        const double heatLossCoefficient = 0.1;
        const double heaterPowerPercentage = 100.0;
        const double ambientTemperature = 19.0;
        const double targetTemperature = 300.0;

        // Act
        var recipe = new Recipe
        {
            Id = id,
            Name = name,
            HeatCapacity = heatCapacity,
            HeatLossCoefficient = heatLossCoefficient,
            HeaterPowerPercentage = heaterPowerPercentage,
            AmbientTemperature = ambientTemperature,
            TargetTemperature = targetTemperature
        };

        // Assert
        using (new AssertionScope())
        {
            recipe.Id.Should().Be(id);
            recipe.Name.Should().Be(name);
            recipe.HeatCapacity.Should().Be(heatCapacity);
            recipe.HeatLossCoefficient.Should().Be(heatLossCoefficient);
            recipe.HeaterPowerPercentage.Should().Be(heaterPowerPercentage);
            recipe.AmbientTemperature.Should().Be(ambientTemperature);
            recipe.TargetTemperature.Should().Be(targetTemperature);
        }
    }
}