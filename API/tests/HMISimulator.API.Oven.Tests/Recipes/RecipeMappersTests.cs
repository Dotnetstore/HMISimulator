using FluentAssertions;
using HMISimulator.API.Contracts.Recipes;
using HMISimulator.API.Oven.Recipes;
using Xunit;

namespace HMISimulator.API.Oven.Tests.Recipes;

public class RecipeMappersTests
{
    [Fact]
    public void ToRecipeResponse_Should_Map_Recipe_To_RecipeResponse()
    {
        // Arrange
        var recipe = new Recipe
        {
            Id = new RecipeId(Guid.NewGuid()),
            Name = "Pizza",
            HeatCapacity = 5000.0,
            HeatLossCoefficient = 0.1,
            HeaterPowerPercentage = 100.0,
            AmbientTemperature = 19.0,
            TargetTemperature = 300.0
        };

        // Act
        var response = recipe.ToRecipeResponse();

        // Assert
        using (new FluentAssertions.Execution.AssertionScope())
        {
            response.Id.Should().Be(recipe.Id.Value);
            response.Name.Should().Be(recipe.Name);
            response.HeatCapacity.Should().Be(recipe.HeatCapacity);
            response.HeatLossCoefficient.Should().Be(recipe.HeatLossCoefficient);
            response.HeaterPowerPercentage.Should().Be(recipe.HeaterPowerPercentage);
            response.AmbientTemperature.Should().Be(recipe.AmbientTemperature);
            response.TargetTemperature.Should().Be(recipe.TargetTemperature);
        }
    }
}