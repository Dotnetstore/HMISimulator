using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Recipe.Requests;
using HMISimulator.API.SDK.Recipe.Responses;
using HMISimulator.API.SharedKernel.Services;
using Microsoft.AspNetCore.Http;

namespace HMISimulator.API.Oven.Recipes.Create;

internal sealed class CreateRecipeEndpoint(IRecipeService recipeService) : Endpoint<CreateRecipeRequest, RecipeResponse?>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Recipe.Create);
        Summary(s =>
            s.ExampleRequest = new CreateRecipeRequest
        {
            Name = DataSchemeConstants.DefaultRecipeNameValue,
            AmbientTemperature = DataSchemeConstants.DefaultAmbientTemperatureValue,
            HeatCapacity = DataSchemeConstants.DefaultHeatCapacityValue,
            HeaterPowerPercentage = DataSchemeConstants.DefaultHeaterPowerPercentageValue,
            HeatLossCoefficient = DataSchemeConstants.DefaultHeatLossCoefficientValue,
            TargetTemperature = DataSchemeConstants.DefaultTargetTemperatureValue
        });
        Description(x =>
            x.WithDescription("Create a new recipe")
             .AutoTagOverride("Recipes"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateRecipeRequest req, CancellationToken ct)
    {
        var recipe = await recipeService.CreateAsync(req, ct);

        if (!recipe.IsSuccess)
        {
            AddError(recipe.Errors.First());
            await SendErrorsAsync(cancellation: ct);
        }
        else
        {
            await SendAsync(recipe.Value, statusCode: 201, cancellation: ct);
        }
    }
}