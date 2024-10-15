using Ardalis.Result;
using HMISimulator.API.Oven.Recipes;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Oven.Requests;

namespace HMISimulator.API.Oven.Ovens;

internal sealed class OvenService(
    IRecipeRepository recipeRepository,
    IOvenSimulator ovenSimulator) : IOvenService
{
    async ValueTask<Result<bool>> IOvenService.LoadRecipeAsync(string recipeName)
    {
        var recipe = await recipeRepository.GetByNameAsync(recipeName);

        if (recipe is null)
        {
            return Result<bool>.NotFound();
        }
        
        ovenSimulator.ActiveRecipe = recipe;

        return Result<bool>.Success(true);
    }

    Result<bool> IOvenService.StartOven()
    {
        if (ovenSimulator.ActiveRecipe is null)
        {
            return Result<bool>.NotFound("No recipe is loaded. Load a recipe first.");
        }
        
        if(ovenSimulator.HeatingElementOn)
        {
            return Result<bool>.Conflict("Oven is already heating");
        }

        ovenSimulator.StartHeating();

        return Result<bool>.Success(true);
    }
    
    Result<bool> IOvenService.StopOven()
    {
        if (!ovenSimulator.HeatingElementOn)
        {
            return Result<bool>.Conflict("Oven is already stopped");
        }

        ovenSimulator.StopHeating();

        return Result<bool>.Success(true);
    }
    
    void IOvenService.AddError(AddErrorRequest req)
    {
        ovenSimulator.SimulateError(req.ErrorType);
    }
}