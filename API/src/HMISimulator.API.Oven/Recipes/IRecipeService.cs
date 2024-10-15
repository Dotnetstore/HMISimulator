using Ardalis.Result;
using HMISimulator.API.SDK.Recipe.Requests;
using HMISimulator.API.SDK.Recipe.Responses;

namespace HMISimulator.API.Oven.Recipes;

internal interface IRecipeService
{
    ValueTask<IEnumerable<RecipeResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    
    ValueTask<Result<RecipeResponse?>> CreateAsync(CreateRecipeRequest request, CancellationToken cancellationToken = default);
}