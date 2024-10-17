using MediatR;

namespace HMISimulator.API.Contracts.Recipes;

public record struct CreateRecipeQuery(string Body) : IRequest;