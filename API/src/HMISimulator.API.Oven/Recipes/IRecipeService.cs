using HMISimulator.API.SDK.Recipe.Responses;

namespace HMISimulator.API.Oven.Recipes;

internal interface IRecipeService
{
    ValueTask<IEnumerable<RecipeResponse>> GetAllAsync(CancellationToken cancellationToken = default);
}