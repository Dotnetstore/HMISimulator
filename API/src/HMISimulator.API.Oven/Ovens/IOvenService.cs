using Ardalis.Result;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Oven.Requests;

namespace HMISimulator.API.Oven.Ovens;

internal interface IOvenService
{
    ValueTask<Result<bool>> LoadRecipeAsync(string recipeName);
    
    Result<bool> StartOven();
    
    Result<bool> StopOven();
    
    void AddError(AddErrorRequest req);
}