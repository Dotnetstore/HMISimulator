using FluentAssertions;
using FluentAssertions.Execution;
using HMISimulator.API.Contracts.Recipes;
using HMISimulator.API.Oven.Recipes.GetAll;
using Xunit;

namespace HMISimulator.API.Oven.Tests.Recipes.GetAll;

public class RecipeBuilderTests
{
    [Fact]
    public void Build_Should_Create_Recipe_With_Valid_Properties()
    {
        // Arrange
        var recipeId = new RecipeId(Guid.NewGuid());
        const string recipeName = "Pizza";
        const double heatCapacity = 5000.0;
        const double heatLossCoefficient = 0.1;
        const double heaterPowerPercentage = 100.0;
        const double ambientTemperature = 19.0;
        const double targetTemperature = 300.0;

        // Act
        var recipe = RecipeBuilder.Create()
            .WithRecipeId(recipeId)
            .WithRecipeName(recipeName)
            .WithHeatCapacity(heatCapacity)
            .WithHeatLossCoefficient(heatLossCoefficient)
            .WithHeaterPowerPercentage(heaterPowerPercentage)
            .WithAmbientTemperature(ambientTemperature)
            .WithTargetTemperature(targetTemperature)
            .Build();

        // Assert
        using (new AssertionScope())
        {
            recipe.Id.Should().Be(recipeId);
            recipe.Name.Should().Be(recipeName);
            recipe.HeatCapacity.Should().Be(heatCapacity);
            recipe.HeatLossCoefficient.Should().Be(heatLossCoefficient);
            recipe.HeaterPowerPercentage.Should().Be(heaterPowerPercentage);
            recipe.AmbientTemperature.Should().Be(ambientTemperature);
            recipe.TargetTemperature.Should().Be(targetTemperature);
        }
    }

    [Fact]
    public void WithRecipeName_Should_Throw_Exception_For_Invalid_Name()
    {
        // Arrange
        var builder = RecipeBuilder.Create();

        // Act
        Action act = () => builder.WithRecipeName("");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*RecipeName*");
    }

    [Fact]
    public void WithHeatCapacity_Should_Throw_Exception_For_Invalid_HeatCapacity()
    {
        // Arrange
        var builder = RecipeBuilder.Create();

        // Act
        Action act = () => builder.WithHeatCapacity(-1);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*HeatCapacity*");
    }
    
    [Fact]
    public void WithHeatLossCoefficient_Should_Throw_Exception_For_Invalid_HeatLossCoefficient()
    {
        // Arrange
        var builder = RecipeBuilder.Create();

        // Act
        Action act = () => builder.WithHeatLossCoefficient(-0.1);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*HeatLossCoefficient*");
    }
    
    [Fact]
    public void WithHeaterPowerPercentage_Should_Throw_Exception_For_Invalid_HeaterPowerPercentage()
    {
        // Arrange
        var builder = RecipeBuilder.Create();

        // Act
        Action act = () => builder.WithHeaterPowerPercentage(-10);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*HeaterPowerPercentage*");
    }
    
    [Fact]
    public void WithAmbientTemperature_Should_Throw_Exception_For_Invalid_AmbientTemperature()
    {
        // Arrange
        var builder = RecipeBuilder.Create();

        // Act
        Action act = () => builder.WithAmbientTemperature(-300);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*AmbientTemperature*");
    }

    [Fact]
    public void WithTargetTemperature_Should_Throw_Exception_For_Invalid_TargetTemperature()
    {
        // Arrange
        var builder = RecipeBuilder.Create();

        // Act
        Action act = () => builder.WithTargetTemperature(-300);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*TargetTemperature*");
    }
}