using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Recipe.Responses;
using Microsoft.AspNetCore.Http;

namespace HMISimulator.API.Oven.Recipes.GetAll;

internal sealed class GetAllRecipeEndpoint(IRecipeService recipeService) : EndpointWithoutRequest<IEnumerable<RecipeResponse>>
{
    public override void Configure()
    {
        Get(ApiEndpoints.Recipe.GetAll);
        Description(x =>
            x.WithDescription("Get all recipes")
             .AutoTagOverride("Recipes"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var recipes = await recipeService.GetAllAsync(ct);
        await SendAsync(recipes, statusCode: 200, ct);
    }
}