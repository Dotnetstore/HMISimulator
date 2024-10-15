using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.SDK;
using Microsoft.AspNetCore.Http;

namespace HMISimulator.API.Oven.Ovens.StartOven;

internal sealed class StartOvenEndpoint(IOvenService ovenService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.Start);
        Description(x =>
            x.WithDescription("Start oven")
                .AutoTagOverride("Oven"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = ovenService.StartOven();

        if (result.IsSuccess)
            await SendOkAsync(ct);
        else
        {
            AddError(string.Join(", ", result.Errors));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}