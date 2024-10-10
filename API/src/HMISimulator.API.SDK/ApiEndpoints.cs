namespace HMISimulator.API.SDK;

public static class ApiEndpoints
{
    private const string Api = "/api";
    
    public static class Oven
    {
        private const string OvenBase = $"{Api}/ovens";
        
        public const string Start = $"{OvenBase}/start";
        public const string Stop = $"{OvenBase}/stop";
        public const string Get = OvenBase;
        public const string AddRecipe = $"{OvenBase}/add-recipe";
        public const string AddError = $"{OvenBase}/add-error";
    }
    
    public static class Recipe
    {
        private const string RecipeBase = $"{Api}/recipes";
        
        public const string GetAll = RecipeBase;
    }
}