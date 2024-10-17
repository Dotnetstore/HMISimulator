using FastEndpoints.Testing;

namespace HMISimulator.API.TestHelper;

public abstract class HMISimulatorAPIBase : AppFixture<Program>
{
    protected override async Task PreSetupAsync()
    {
        await Task.CompletedTask;
    }

    protected override async Task TearDownAsync()
    {
        await Task.CompletedTask;
    }
}