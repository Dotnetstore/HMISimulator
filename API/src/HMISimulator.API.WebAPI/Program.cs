using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using HMISimulator.API.Oven.Extensions;
using HMISimulator.API.SharedKernel.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument()
    .AddSharedKernel()
    .AddOven(builder.Configuration)
    .AddHealthChecks();

var app = builder.Build();

app
    .UseFastEndpoints()
    .UseSwaggerGen()
    .UseHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

namespace HMISimulator.API.WebAPI
{
    public partial class Program;
}