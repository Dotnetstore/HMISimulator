using HMISimulator.API.Oven.Recipes.Create;
using HMISimulator.API.SharedKernel.Models;
using HMISimulator.API.SharedKernel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HMISimulator.API.Oven.Recipes;

internal sealed class RecipeConfiguration : BaseAuditableEntityConfiguration<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        base.Configure(builder);
        
        var converter = new ValueConverter<RecipeId, Guid>(
            id => id.Value, 
            guid => new RecipeId(guid));

        builder
            .HasIndex(x => x.Id)
            .IsUnique()
            .HasDatabaseName("Index_Id");
        
        builder
            .HasIndex(x => x.Name)
            .IsUnique()
            .HasDatabaseName("Index_Name");
        
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .HasConversion(converter)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasMaxLength(DataSchemeConstants.NameMaxLength)
            .IsRequired()
            .IsUnicode();

        builder
            .Property(x => x.HeatCapacity)
            .IsRequired();

        builder
            .Property(x => x.HeatLossCoefficient)
            .IsRequired();

        builder
            .Property(x => x.HeaterPowerPercentage)
            .IsRequired();

        builder
            .Property(x => x.AmbientTemperature)
            .IsRequired();

        builder
            .Property(x => x.TargetTemperature)
            .IsRequired();

        builder
            .HasData(
                CreateRecipeBuilder.CreateNewRecipe()
                    .CreateRecipeId(Guid.Parse(DataSchemeConstants.DefaultRecipeIdValue))
                    .SetRecipeName(DataSchemeConstants.DefaultRecipeNameValue)
                    .SetRecipeHeatCapacity(DataSchemeConstants.DefaultHeatCapacityValue)
                    .SetRecipeHeatLossCoefficient(DataSchemeConstants.DefaultHeatLossCoefficientValue)
                    .SetRecipeHeaterPowerPercentage(DataSchemeConstants.DefaultHeaterPowerPercentageValue)
                    .SetRecipeAmbientTemperature(DataSchemeConstants.DefaultAmbientTemperatureValue)
                    .SetRecipeTargetTemperature(DataSchemeConstants.DefaultTargetTemperatureValue)
                    .Build());
    }
}