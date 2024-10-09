using FastEndpoints;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Oven.Requests;

namespace HMISimulator.API.Oven;

internal sealed class AddErrorEndPoint(IOvenSimulator ovenSimulator) : Endpoint<AddErrorRequest>
{
    public override void Configure()
    {
        Post("/oven/add-error");
        Summary(s =>
            s.ExampleRequest = new AddErrorRequest
            {
                ErrorType = OvenErrorType.None
            });
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddErrorRequest req, CancellationToken ct)
    {
        ovenSimulator.SimulateError(req.ErrorType);
        await SendOkAsync(ct);
    }
}