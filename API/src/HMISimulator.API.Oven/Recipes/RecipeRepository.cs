using Microsoft.EntityFrameworkCore;

namespace HMISimulator.API.Oven.Recipes;

internal sealed class RecipeRepository(IOvenUnitOfWork unitOfWork) : IRecipeRepository
{
    private IQueryable<Recipe> GetRecipeQuery()
    {
        return unitOfWork
            .Repository<Recipe>()
            .Entities
            .OrderBy(x => x.Name);
    }
    async ValueTask<IEnumerable<Recipe>> IRecipeRepository.GetAllAsync(CancellationToken cancellationToken)
    {
        var query = GetRecipeQuery();
        
        return await query
            .ToListAsync(cancellationToken);
    }

    async ValueTask<Recipe?> IRecipeRepository.GetByIdAsync(RecipeId id, CancellationToken cancellationToken)
    {
        var query = GetRecipeQuery();

        return await query
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    async ValueTask<Recipe?> IRecipeRepository.GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var query = GetRecipeQuery();

        return await query
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync(cancellationToken);
    }

    void IRecipeRepository.Create(Recipe recipe)
    {
        unitOfWork
            .Repository<Recipe>()
            .Create(recipe);
    }

    async ValueTask IRecipeRepository.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}