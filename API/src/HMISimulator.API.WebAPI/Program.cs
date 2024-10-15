using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.Oven.Extensions;
using HMISimulator.API.SharedKernel.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument()
    .AddSharedKernel()
    .AddOven(builder.Configuration);

var app = builder.Build();

app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();

namespace HMISimulator.API.WebAPI
{
    public partial class Program;
}