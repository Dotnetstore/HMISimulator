using Microsoft.EntityFrameworkCore;

namespace HMISimulator.API.Oven.Recipes;

internal sealed class RecipeRepository(IOvenUnitOfWork unitOfWork) : IRecipeRepository
{
    async ValueTask<IEnumerable<Recipe>> IRecipeRepository.GetAllAsync(CancellationToken cancellationToken)
    {
        return await unitOfWork
            .Repository<Recipe>()
            .Entities
            .ToListAsync(cancellationToken);
    }
}