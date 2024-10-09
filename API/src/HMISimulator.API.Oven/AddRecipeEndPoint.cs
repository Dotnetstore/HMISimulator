using FastEndpoints;
using HMISimulator.API.SDK.Oven.Requests;

namespace HMISimulator.API.Oven;

internal sealed class AddRecipeEndPoint(IOvenSimulator ovenSimulator) : Endpoint<AddRecipeRequest>
{
     public override void Configure()
     {
         Post("/oven/add-recipe");
         Summary(s =>
             s.ExampleRequest = new AddRecipeRequest
             {
                    HeatCapacity = 5000.0,
                    HeatLossCoefficient = 0.01,
                    HeaterPowerPercentage = 100.0,
                    AmbientTemperature = 25.0,
                    TargetTemperature = 300.0
             });
         AllowAnonymous();
     }

     public override async Task HandleAsync(AddRecipeRequest req, CancellationToken ct)
     {
         ovenSimulator.HeatCapacity = req.HeatCapacity;
         ovenSimulator.HeatLossCoefficient = req.HeatLossCoefficient;
         ovenSimulator.HeaterPowerPercentage = req.HeaterPowerPercentage;
         ovenSimulator.AmbientTemperature = req.AmbientTemperature;
         ovenSimulator.TargetTemperature = req.TargetTemperature;
         
         await SendOkAsync(ct);
     }
}