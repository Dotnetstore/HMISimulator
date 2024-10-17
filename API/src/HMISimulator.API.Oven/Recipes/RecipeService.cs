using System.Text.Json;
using Ardalis.Result;
using HMISimulator.API.Contracts.Recipes;
using HMISimulator.API.Oven.Recipes.Create;
using HMISimulator.API.SDK.Recipe.Requests;
using HMISimulator.API.SDK.Recipe.Responses;
using MediatR;

namespace HMISimulator.API.Oven.Recipes;

internal sealed class RecipeService(
    IRecipeRepository recipeRepository,
    ISender sender) : IRecipeService
{
    async ValueTask<IEnumerable<RecipeResponse>> IRecipeService.GetAllAsync(CancellationToken cancellationToken)
    {
        var recipes = await recipeRepository.GetAllAsync(cancellationToken);
        return recipes.Select(recipe => recipe.ToRecipeResponse());
    }

    async ValueTask<Result<RecipeResponse?>> IRecipeService.CreateAsync(CreateRecipeRequest request, CancellationToken cancellationToken)
    {
        var existingRecipe = await recipeRepository.GetByNameAsync(request.Name, cancellationToken);

        if (existingRecipe != null)
            return Result.Conflict("Recipe already exists");
        
        var recipe = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(Guid.NewGuid())
            .SetRecipeName(request.Name)
            .SetRecipeHeatCapacity(request.HeatCapacity)
            .SetRecipeHeatLossCoefficient(request.HeatLossCoefficient)
            .SetRecipeHeaterPowerPercentage(request.HeaterPowerPercentage)
            .SetRecipeAmbientTemperature(request.AmbientTemperature)
            .SetRecipeTargetTemperature(request.TargetTemperature)
            .Build();
        
        recipeRepository.Create(recipe);
        await recipeRepository.SaveChangesAsync(cancellationToken);
        
        var createdRecipe = await recipeRepository.GetByIdAsync(recipe.Id, cancellationToken);
        
        if (createdRecipe is null)
            return Result.Error("Failed to create recipe");

        var query = new CreateRecipeQuery(JsonSerializer.Serialize(createdRecipe));
        await sender.Send(query, cancellationToken);
        
        return createdRecipe.ToRecipeResponse();
    }
}