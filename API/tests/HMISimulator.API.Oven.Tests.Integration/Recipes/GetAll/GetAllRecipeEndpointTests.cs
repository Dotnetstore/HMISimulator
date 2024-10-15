using System.Net;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using HMISimulator.API.Oven.Recipes.GetAll;
using HMISimulator.API.SDK.Recipe.Responses;
using Xunit;

namespace HMISimulator.API.Oven.Tests.Integration.Recipes.GetAll;

public class GetAllRecipeEndpointTests(
    HMISimulatorAPIOvenBase hmiSimulatorApiOvenBase)
    : TestBase<HMISimulatorAPIOvenBase>
{
    
    [Fact]
    public async Task Get_ReturnsOk()
    {
        // Act
        var (rsp, res) = await hmiSimulatorApiOvenBase.Client.GETAsync<GetAllRecipeEndpoint, IEnumerable<RecipeResponse>>();

        // Assert
        rsp.StatusCode.Should().Be(HttpStatusCode.OK);
        res.Should().NotBeNull();
    }
}