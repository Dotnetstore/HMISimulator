using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Oven.Responses;
using Microsoft.AspNetCore.Http;

namespace HMISimulator.API.Oven.Ovens.GetStatus;

internal sealed class GetOvenStatusEndpoint(IOvenSimulator ovenSimulator) : EndpointWithoutRequest<GetOvenStatusResponse>
{
    public override void Configure()
    {
        Get(ApiEndpoints.Oven.Get);
        Description(x =>
            x.WithDescription("Get oven status")
                .AutoTagOverride("Oven"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new GetOvenStatusResponse(
            ovenSimulator.CurrentTemperature,
            ovenSimulator.HeatingElementOn,
            ovenSimulator.ActiveRecipe?.Name ?? "No active recipe",
            ovenSimulator.CurrentError.ToString());

        await SendAsync(response, cancellation: ct);
    }
}