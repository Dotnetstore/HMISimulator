using HMISimulator.API.SDK.Recipe.Responses;

namespace HMISimulator.API.Oven.Recipes;

internal static class RecipeMappers
{
    internal static RecipeResponse ToRecipeResponse(this Recipe recipe)
    {
        return new RecipeResponse(
            recipe.Id.Value,
            recipe.Name,
            recipe.HeatCapacity,
            recipe.HeatLossCoefficient,
            recipe.HeaterPowerPercentage,
            recipe.AmbientTemperature,
            recipe.TargetTemperature);
    }   
}