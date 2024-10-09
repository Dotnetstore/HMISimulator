using FastEndpoints;

namespace HMISimulator.API.Oven;

internal sealed class StartOvenEndpoint(IOvenSimulator ovenSimulator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/oven/start");
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