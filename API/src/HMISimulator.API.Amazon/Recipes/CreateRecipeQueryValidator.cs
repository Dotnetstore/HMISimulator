using FluentValidation;
using HMISimulator.API.Contracts.Recipes;

namespace HMISimulator.API.Amazon.Recipes;

public sealed class CreateRecipeQueryValidator : AbstractValidator<CreateRecipeQuery>
{
    public CreateRecipeQueryValidator()
    {
        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage("Body is required.");
    }
}