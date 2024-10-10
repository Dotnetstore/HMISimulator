namespace HMISimulator.API.Oven.Recipes;

internal interface IRecipeRepository
{
    ValueTask<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);
}