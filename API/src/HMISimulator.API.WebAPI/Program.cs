using FastEndpoints;
using FastEndpoints.Swagger;
using HMISimulator.API.Oven;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument()
    .AddOven();

var app = builder.Build();

app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();