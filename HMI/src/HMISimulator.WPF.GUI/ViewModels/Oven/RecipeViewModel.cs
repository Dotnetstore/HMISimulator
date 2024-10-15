using System.Collections.ObjectModel;
using HMISimulator.API.SDK;
using HMISimulator.API.SDK.Recipe.Responses;
using HMISimulator.WPF.GUI.ViewModels.Main;

namespace HMISimulator.WPF.GUI.ViewModels.Oven;

public sealed class RecipeViewModel(IOvenService ovenService) : BaseViewModel, IRecipeViewModel
{
    private ObservableCollection<RecipeResponse> _recipes = null!;

    public ObservableCollection<RecipeResponse> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }

    async ValueTask IRecipeViewModel.LoadAsync()
    {
        await LoadRecipesAsync();
    }
    
    private async ValueTask LoadRecipesAsync()
    {
        var recipes = await ovenService.GetAllRecipesAsync();
        Recipes = new ObservableCollection<RecipeResponse>(recipes);
    }
}