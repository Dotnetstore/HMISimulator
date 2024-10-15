using FluentAssertions;
using HMISimulator.API.Oven.Recipes.Create;
using Xunit;

namespace HMISimulator.API.Oven.Tests.Recipes.Create;

public class CreateRecipeBuilderTests
{
    [Fact]
    public void Build_Should_Create_Recipe_With_Valid_Properties()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        const string recipeName = "Pizza";
        const double heatCapacity = 5000.0;
        const double heatLossCoefficient = 0.1;
        const double heaterPowerPercentage = 100.0;
        const double ambientTemperature = 19.0;
        const double targetTemperature = 300.0;

        // Act
        var recipe = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(recipeId)
            .SetRecipeName(recipeName)
            .SetRecipeHeatCapacity(heatCapacity)
            .SetRecipeHeatLossCoefficient(heatLossCoefficient)
            .SetRecipeHeaterPowerPercentage(heaterPowerPercentage)
            .SetRecipeAmbientTemperature(ambientTemperature)
            .SetRecipeTargetTemperature(targetTemperature)
            .Build();

        // Assert
        using (new FluentAssertions.Execution.AssertionScope())
        {
            recipe.Id.Value.Should().Be(recipeId);
            recipe.Name.Should().Be(recipeName);
            recipe.HeatCapacity.Should().Be(heatCapacity);
            recipe.HeatLossCoefficient.Should().Be(heatLossCoefficient);
            recipe.HeaterPowerPercentage.Should().Be(heaterPowerPercentage);
            recipe.AmbientTemperature.Should().Be(ambientTemperature);
            recipe.TargetTemperature.Should().Be(targetTemperature);
        }
    }

    [Fact]
    public void SetRecipeName_Should_Throw_Exception_For_Invalid_Name()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe().CreateRecipeId(Guid.NewGuid());

        // Act
        Action act = () => builder.SetRecipeName("");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*recipeName*");
    }

    [Fact]
    public void SetRecipeHeatCapacity_Should_Throw_Exception_For_Invalid_HeatCapacity()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe().CreateRecipeId(Guid.NewGuid()).SetRecipeName("Pizza");

        // Act
        Action act = () => builder.SetRecipeHeatCapacity(-1);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*heatCapacity*");
    }

    [Fact]
    public void SetRecipeHeatLossCoefficient_Should_Throw_Exception_For_Invalid_HeatLossCoefficient()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe().CreateRecipeId(Guid.NewGuid()).SetRecipeName("Pizza").SetRecipeHeatCapacity(5000.0);

        // Act
        Action act = () => builder.SetRecipeHeatLossCoefficient(-0.1);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*heatLossCoefficient*");
    }

    [Fact]
    public void SetRecipeHeaterPowerPercentage_Should_Throw_Exception_For_Invalid_HeaterPowerPercentage()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe().CreateRecipeId(Guid.NewGuid()).SetRecipeName("Pizza").SetRecipeHeatCapacity(5000.0).SetRecipeHeatLossCoefficient(0.1);

        // Act
        Action act = () => builder.SetRecipeHeaterPowerPercentage(-10);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*heaterPowerPercentage*");
    }

    [Fact]
    public void SetRecipeAmbientTemperature_Should_Throw_Exception_For_Invalid_AmbientTemperature()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe().CreateRecipeId(Guid.NewGuid()).SetRecipeName("Pizza").SetRecipeHeatCapacity(5000.0).SetRecipeHeatLossCoefficient(0.1).SetRecipeHeaterPowerPercentage(100.0);

        // Act
        Action act = () => builder.SetRecipeAmbientTemperature(-300);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*ambientTemperature*");
    }

    [Fact]
    public void SetRecipeTargetTemperature_Should_Throw_Exception_For_Invalid_TargetTemperature()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe().CreateRecipeId(Guid.NewGuid()).SetRecipeName("Pizza").SetRecipeHeatCapacity(5000.0).SetRecipeHeatLossCoefficient(0.1).SetRecipeHeaterPowerPercentage(100.0).SetRecipeAmbientTemperature(19.0);

        // Act
        Action act = () => builder.SetRecipeTargetTemperature(-300);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*targetTemperature*");
    }
}