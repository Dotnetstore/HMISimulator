using Amazon.SQS.Model;

namespace HMISimulator.API.Amazon.Services;

internal interface IAmazonSqsService
{
    ValueTask<string> CreateQueueWithName(string queueName, bool useFifoQueue);

    ValueTask<SendMessageResponse> SendMessageAsync(string messageBody, string operationName, CancellationToken cancellationToken = default);
}