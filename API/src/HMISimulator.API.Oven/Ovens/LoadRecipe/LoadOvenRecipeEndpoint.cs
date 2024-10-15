using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Oven.Requests;
using Microsoft.AspNetCore.Http;

namespace HMISimulator.API.Oven.Ovens.LoadRecipe;

internal sealed class LoadOvenRecipeEndpoint(IOvenService ovenService) : Endpoint<LoadRecipeRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.LoadRecipe);
        Summary(s =>
            s.ExampleRequest = new LoadRecipeRequest
            {
                Name = "Pizza"
            });
        Description(x =>
            x.WithDescription("Load a recipe to be used by the oven")
                .AutoTagOverride("Oven"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoadRecipeRequest req, CancellationToken ct)
    {
        var result = await ovenService.LoadRecipeAsync(req.Name);

        if (result.IsSuccess)
            await SendOkAsync(ct);
        else
        {
            AddError(string.Join(", ", result.Errors));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}