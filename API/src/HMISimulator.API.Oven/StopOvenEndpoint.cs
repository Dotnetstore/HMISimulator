using FastEndpoints;

namespace HMISimulator.API.Oven;

internal sealed class StopOvenEndpoint(IOvenSimulator ovenSimulator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/oven/stop");
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