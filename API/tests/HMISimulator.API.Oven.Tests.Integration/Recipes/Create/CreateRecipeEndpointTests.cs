using System.Net;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using HMISimulator.API.Oven.Recipes.Create;
using HMISimulator.API.SDK.Recipe.Requests;
using HMISimulator.API.SDK.Recipe.Responses;
using Xunit;

namespace HMISimulator.API.Oven.Tests.Integration.Recipes.Create;

public class CreateRecipeEndpointTests(
    HMISimulatorAPIOvenBase hmiSimulatorApiOvenBase)
    : TestBase<HMISimulatorAPIOvenBase>
{
    [Fact]
    public async Task Create_ReturnsCreated()
    {
        // Act
        var (rsp, res) = await hmiSimulatorApiOvenBase.Client.POSTAsync<CreateRecipeEndpoint, CreateRecipeRequest, RecipeResponse?>(new CreateRecipeRequest
        {
            Name = "Test Recipe",
            AmbientTemperature = 20,
            HeatCapacity = 5000,
            HeaterPowerPercentage = 50,
            HeatLossCoefficient = 0.1,
            TargetTemperature = 300
        });

        // Assert
        rsp.StatusCode.Should().Be(HttpStatusCode.Created);
        res.Should().NotBeNull();
    }
}