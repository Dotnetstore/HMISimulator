using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;

namespace HMISimulator.API.WebAPI.Extensions;

internal static class StartupApplicationExtensions
{
    internal static void StartupApplication(this WebApplicationBuilder builder)
    {
        builder
            .SetupLogging();
        
        builder
            .Services
            .AddWebApi(builder.Configuration);
        
        var app = builder.BuildApplication();
        
        app
            .UseFastEndpoints()
            .UseSwaggerGen()
            .UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        app.Run();
    }
    
    private static WebApplicationBuilder SetupLogging(this WebApplicationBuilder builder)
    {
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

        return builder;
    }
    
    private static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        return builder.Build();
    }
}