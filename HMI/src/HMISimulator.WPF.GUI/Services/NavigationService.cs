namespace HMISimulator.WPF.GUI.Services;

internal sealed class NavigationService : INavigationService
{
    public event EventHandler? NavigateToOvenControl;
    public event EventHandler? NavigateToRecipe;
    
    void INavigationService.OnNavigateToOvenControl()
    {
        NavigateToOvenControl?.Invoke(this, EventArgs.Empty);
    }

    void INavigationService.OnNavigateToRecipe()
    {
        NavigateToRecipe?.Invoke(this, EventArgs.Empty);
    }
}