using HMISimulator.WPF.GUI.ViewModels.Main;

namespace HMISimulator.WPF.GUI.ViewModels.Oven;

public interface IRecipeViewModel : IBaseViewModel
{
    ValueTask LoadAsync();
}