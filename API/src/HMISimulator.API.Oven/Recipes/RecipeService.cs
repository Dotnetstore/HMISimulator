using HMISimulator.API.SDK.Recipe.Responses;

namespace HMISimulator.API.Oven.Recipes;

internal sealed class RecipeService(IRecipeRepository recipeRepository) : IRecipeService
{
    async ValueTask<IEnumerable<RecipeResponse>> IRecipeService.GetAllAsync(CancellationToken cancellationToken)
    {
        var recipes = await recipeRepository.GetAllAsync(cancellationToken);
        return recipes.Select(recipe => recipe.ToRecipeResponse());
    }
}