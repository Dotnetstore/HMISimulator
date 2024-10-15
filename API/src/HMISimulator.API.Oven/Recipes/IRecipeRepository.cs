namespace HMISimulator.API.Oven.Recipes;

internal interface IRecipeRepository
{
    ValueTask<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);
    
    ValueTask<Recipe?> GetByIdAsync(RecipeId id, CancellationToken cancellationToken = default);
    
    ValueTask<Recipe?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    void Create(Recipe recipe);
    
    ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
}