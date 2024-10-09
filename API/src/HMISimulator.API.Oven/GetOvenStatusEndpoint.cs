using FastEndpoints;
using HMISimulator.API.SDK.Oven.Responses;

namespace HMISimulator.API.Oven;

internal sealed class GetOvenStatusEndpoint(IOvenSimulator ovenSimulator) : EndpointWithoutRequest<GetOvenStatusResponse>
{
    public override void Configure()
    {
        Get("/oven/status");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new GetOvenStatusResponse(
            ovenSimulator.CurrentTemperature,
            ovenSimulator.HeatingElementOn);

        await SendAsync(response, cancellation: ct);
    }
}