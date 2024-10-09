using FastEndpoints;
using HMISimulator.API.SDK;

namespace HMISimulator.API.Oven;

internal sealed class StopOvenEndpoint(IOvenSimulator ovenSimulator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.Stop);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (ovenSimulator.HeatingElementOn)
        {
            ovenSimulator.StopHeating();
        }
        
        await SendOkAsync(ct);
    }
}