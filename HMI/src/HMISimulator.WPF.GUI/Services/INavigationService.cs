namespace HMISimulator.WPF.GUI.Services;

internal interface INavigationService
{
    event EventHandler NavigateToOvenControl;
    event EventHandler NavigateToRecipe;
    
    void OnNavigateToOvenControl();
    void OnNavigateToRecipe();
}