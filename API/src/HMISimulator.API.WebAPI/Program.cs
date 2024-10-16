using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using HMISimulator.API.Oven.Extensions;
using HMISimulator.API.SharedKernel.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(x =>
{
    x.IncludeScopes = true;
    x.IncludeFormattedMessage = true;

    x.AddOtlpExporter(a =>
    {
        a.Endpoint = new Uri("http://localhost:5341/ingest/otlp/v1/logs");
        a.Protocol = OtlpExportProtocol.HttpProtobuf;
        a.Headers = "X-Seq-ApiKey=H1fNIbtzpfcgbhEMrfpo";
    });
});

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