using HMISimulator.API.SDK.Oven.Requests;
using HMISimulator.API.SDK.Oven.Responses;
using Refit;

namespace HMISimulator.API.SDK;

[Headers("application/json", "Authorization: Bearer")]
public interface IOvenService
{
    [Get(ApiEndpoints.Oven.Get)]
    Task<GetOvenStatusResponse> GetOvenStatusAsync();
    
    [Post(ApiEndpoints.Oven.AddRecipe)]
    Task AddRecipeAsync([Body] AddRecipeRequest request);
    
    [Post(ApiEndpoints.Oven.Start)]
    Task StartAsync();
    
    [Post(ApiEndpoints.Oven.Stop)]
    Task StopAsync();
    
    [Post(ApiEndpoints.Oven.AddError)]
    Task AddErrorAsync([Body] AddErrorRequest request);
}