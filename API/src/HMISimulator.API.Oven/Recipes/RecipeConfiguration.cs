using HMISimulator.API.SharedKernel.Models;
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
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .HasConversion(converter)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasMaxLength(50)
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
    }
}