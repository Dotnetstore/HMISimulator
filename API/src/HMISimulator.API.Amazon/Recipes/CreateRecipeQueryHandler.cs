using HMISimulator.API.Amazon.Services;
using HMISimulator.API.Contracts.Recipes;
using MediatR;

namespace HMISimulator.API.Amazon.Recipes;

internal sealed class CreateRecipeQueryHandler(IAmazonSqsService amazonSqsService) : IRequestHandler<CreateRecipeQuery>
{
    async Task IRequestHandler<CreateRecipeQuery>
        .Handle(CreateRecipeQuery request, CancellationToken cancellationToken)
    {
        await amazonSqsService.SendMessageAsync(request.Body, "CreateRecipe", cancellationToken);
    }
}