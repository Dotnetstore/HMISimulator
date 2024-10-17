using FluentAssertions;
using FluentAssertions.Execution;
using HMISimulator.API.Contracts.Recipes;
using HMISimulator.API.Oven.Recipes;
using HMISimulator.API.SharedKernel.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HMISimulator.API.Oven.Tests.Recipes;

public class RecipeConfigurationTests
{
    private DbContextOptions<RecipeDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<RecipeDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void RecipeConfiguration_Should_Set_Properties_Correctly()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new RecipeDbContext(options);
        context.Database.EnsureCreated();
        var recipe = context.Recipes.FirstOrDefault(r => r.Name == "Pizza");

        // Assert
        using (new AssertionScope())
        {
            recipe.Should().NotBeNull();
            recipe!.AmbientTemperature.Should().Be(DataSchemeConstants.DefaultAmbientTemperatureValue);
            recipe.HeatCapacity.Should().Be(DataSchemeConstants.DefaultHeatCapacityValue);
            recipe.HeatLossCoefficient.Should().Be(DataSchemeConstants.DefaultHeatLossCoefficientValue);
            recipe.HeaterPowerPercentage.Should().Be(DataSchemeConstants.DefaultHeaterPowerPercentageValue);
            recipe.Name.Should().Be(DataSchemeConstants.DefaultRecipeNameValue);
            recipe.TargetTemperature.Should().Be(DataSchemeConstants.DefaultTargetTemperatureValue);
            recipe.Id.Should().Be(new RecipeId(Guid.Parse(DataSchemeConstants.DefaultRecipeIdValue)));
        }
    }
}

public class RecipeDbContext(DbContextOptions<RecipeDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
    }
}