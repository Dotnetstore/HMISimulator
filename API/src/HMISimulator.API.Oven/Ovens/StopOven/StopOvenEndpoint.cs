using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.SDK;
using Microsoft.AspNetCore.Http;

namespace HMISimulator.API.Oven.Ovens.StopOven;

internal sealed class StopOvenEndpoint(IOvenService ovenService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.Stop);
        Description(x =>
            x.WithDescription("Stop oven")
                .AutoTagOverride("Oven"));
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        var result = ovenService.StopOven();

        if (result.IsSuccess)
            return SendOkAsync(ct);
        
        AddError(string.Join(", ", result.Errors));
        return SendErrorsAsync(cancellation: ct);
    }
}