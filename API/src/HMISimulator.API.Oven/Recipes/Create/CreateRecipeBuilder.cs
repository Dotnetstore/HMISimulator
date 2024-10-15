using Ardalis.GuardClauses;
using HMISimulator.API.Oven.Recipes.GetAll;
using HMISimulator.API.SharedKernel.Services;

namespace HMISimulator.API.Oven.Recipes.Create;

internal sealed class CreateRecipeBuilder : 
    ICreateRecipeId,
    ISetRecipeNameObject,
    ISetRecipeHeatCapacityObject,
    ISetRecipeHeatLossCoefficientObject,
    ISetRecipeHeaterPowerPercentageObject,
    ISetRecipeAmbientTemperatureObject,
    ISetRecipeTargetTemperatureObject,
    IBuildRecipe
{
    private RecipeId _recipeId;
    private string _recipeName = null!;
    private double _heatCapacity;
    private double _heatLossCoefficient;
    private double _heaterPowerPercentage;
    private double _ambientTemperature;
    private double _targetTemperature;
    
    private CreateRecipeBuilder()
    {
    }

    internal static ICreateRecipeId CreateNewRecipe()
    {
        return new CreateRecipeBuilder();
    }
    
    ISetRecipeNameObject ICreateRecipeId.CreateRecipeId(Guid id)
    {
        _recipeId = new RecipeId(id);
        return this;
    }

    ISetRecipeHeatCapacityObject ISetRecipeNameObject.SetRecipeName(string recipeName)
    {
        var name = Guard.Against.NullOrEmpty(recipeName, nameof(recipeName));
        name = Guard.Against.StringTooLong(name, DataSchemeConstants.NameMaxLength, nameof(_recipeName));
        _recipeName = name;
        return this;
    }

    ISetRecipeHeatLossCoefficientObject ISetRecipeHeatCapacityObject.SetRecipeHeatCapacity(double heatCapacity)
    {
        var capacity = Guard.Against.NegativeOrZero(heatCapacity, nameof(_heatCapacity));
        capacity = Guard.Against.OutOfRange(capacity, nameof(_heatCapacity), 0.0, 10000.0);
        _heatCapacity = capacity;
        return this;
    }

    ISetRecipeHeaterPowerPercentageObject ISetRecipeHeatLossCoefficientObject.SetRecipeHeatLossCoefficient(double heatLossCoefficient)
    {
        var coefficient = Guard.Against.NegativeOrZero(heatLossCoefficient, nameof(_heatLossCoefficient));
        coefficient = Guard.Against.OutOfRange(coefficient, nameof(_heatLossCoefficient), 0.0, 0.9);
        _heatLossCoefficient = coefficient;
        return this;
    }

    ISetRecipeAmbientTemperatureObject ISetRecipeHeaterPowerPercentageObject.SetRecipeHeaterPowerPercentage(double heaterPowerPercentage)
    {
        var powerPercentage = Guard.Against.NegativeOrZero(heaterPowerPercentage, nameof(_heaterPowerPercentage));
        powerPercentage = Guard.Against.OutOfRange(powerPercentage, nameof(_heaterPowerPercentage), 0.0, 100.0);
        _heaterPowerPercentage = powerPercentage;
        return this;
    }

    ISetRecipeTargetTemperatureObject ISetRecipeAmbientTemperatureObject.SetRecipeAmbientTemperature(double ambientTemperature)
    {
        var roomTemperature = Guard.Against.NegativeOrZero(ambientTemperature, nameof(_ambientTemperature));
        roomTemperature = Guard.Against.OutOfRange(roomTemperature, nameof(_ambientTemperature), 0.0, 50.0);
        _ambientTemperature = roomTemperature;
        return this;
    }

    IBuildRecipe ISetRecipeTargetTemperatureObject.SetRecipeTargetTemperature(double targetTemperature)
    {
        var target = Guard.Against.NegativeOrZero(targetTemperature, nameof(_targetTemperature));
        target = Guard.Against.OutOfRange(target, nameof(_targetTemperature), 0.0, _heatCapacity);
        _targetTemperature = target;
        return this;
    }

    Recipe IBuildRecipe.Build()
    {
        return RecipeBuilder.Create()
            .WithRecipeId(_recipeId)
            .WithRecipeName(_recipeName)
            .WithHeatCapacity(_heatCapacity)
            .WithHeatLossCoefficient(_heatLossCoefficient)
            .WithHeaterPowerPercentage(_heaterPowerPercentage)
            .WithAmbientTemperature(_ambientTemperature)
            .WithTargetTemperature(_targetTemperature)
            .Build();
    }
}

internal interface ICreateRecipeId
{
    ISetRecipeNameObject CreateRecipeId(Guid id);
}

internal interface ISetRecipeNameObject
{
    ISetRecipeHeatCapacityObject SetRecipeName(string recipeName);
}

internal interface ISetRecipeHeatCapacityObject
{
    ISetRecipeHeatLossCoefficientObject SetRecipeHeatCapacity(double heatCapacity);
}

internal interface ISetRecipeHeatLossCoefficientObject
{
    ISetRecipeHeaterPowerPercentageObject SetRecipeHeatLossCoefficient(double heatLossCoefficient);
}

internal interface ISetRecipeHeaterPowerPercentageObject
{
    ISetRecipeAmbientTemperatureObject SetRecipeHeaterPowerPercentage(double heaterPowerPercentage);
}

internal interface ISetRecipeAmbientTemperatureObject
{
    ISetRecipeTargetTemperatureObject SetRecipeAmbientTemperature(double ambientTemperature);
}

internal interface ISetRecipeTargetTemperatureObject
{
    IBuildRecipe SetRecipeTargetTemperature(double targetTemperature);
}

internal interface IBuildRecipe
{
    Recipe Build();
}