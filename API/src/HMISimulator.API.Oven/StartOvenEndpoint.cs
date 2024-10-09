using FastEndpoints;
using HMISimulator.API.SDK;

namespace HMISimulator.API.Oven;

internal sealed class StartOvenEndpoint(IOvenSimulator ovenSimulator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.Start);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (!ovenSimulator.HeatingElementOn)
        {
            ovenSimulator.StartHeating();
        }
        
        await SendOkAsync(ct);
    }
}