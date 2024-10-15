using HMISimulator.API.Oven.Services;
using HMISimulator.API.SharedKernel.Extensions;
using HMISimulator.API.TestHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace HMISimulator.API.Oven.Tests.Integration;

public class HMISimulatorAPIOvenBase : HMISimulatorAPIBase
{
    protected override void ConfigureApp(IWebHostBuilder a)
    {
        a.ConfigureTestServices(x =>
        {
            x.RemoveDbContext<OvenDataContext>();
            x.AddDbContext<OvenDataContext>("DataSource=file::memory:?cache=shared");
            // x.EnsureDbMigrated<OvenDataContext>();
            x.EnsureDbDeleted<OvenDataContext>();
            x.EnsureDbCreated<OvenDataContext>();
        });
    }
}