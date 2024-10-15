using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Oven.Requests;
using Microsoft.AspNetCore.Http;

namespace HMISimulator.API.Oven.Ovens.AddError;

internal sealed class AddErrorEndpoint(IOvenService ovenService) : Endpoint<AddErrorRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.AddError);
        Summary(s =>
            s.ExampleRequest = new AddErrorRequest
            {
                ErrorType = OvenErrorType.None
            });
        Description(x =>
            x.WithDescription("Add error to the oven")
                .AutoTagOverride("Oven"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddErrorRequest req, CancellationToken ct)
    {
        ovenService.AddError(req);
        
        await SendOkAsync(ct);
    }
}