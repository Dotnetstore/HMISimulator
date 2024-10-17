using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;

namespace HMISimulator.API.Amazon.Services;

internal sealed class AmazonSqsService(IConfiguration configuration) : IAmazonSqsService
{
    private readonly AmazonSQSClient _amazonSqsClient = new(RegionEndpoint.EUNorth1);
    
    async ValueTask<string> IAmazonSqsService.CreateQueueWithName(string queueName, bool useFifoQueue)
    {
        const int maxMessage = 256 * 1024;
        var queueAttributes = new Dictionary<string, string>
        {
            {
                QueueAttributeName.MaximumMessageSize,
                maxMessage.ToString()
            }
        };

        var createQueueRequest = new CreateQueueRequest
        {
            QueueName = queueName,
            Attributes = queueAttributes
        };

        if (useFifoQueue)
        {
            if (!queueName.EndsWith(".fifo"))
            {
                createQueueRequest.QueueName = $"{queueName}.fifo";
            }

            createQueueRequest.Attributes.Add(QueueAttributeName.FifoQueue, "true");
        }

        var createResponse = await _amazonSqsClient.CreateQueueAsync(
            new CreateQueueRequest
            {
                QueueName = queueName
            });
        return createResponse.QueueUrl;
    }
    
    async ValueTask<SendMessageResponse> IAmazonSqsService.SendMessageAsync(string messageBody, string operationName, CancellationToken cancellationToken)
    {
        var queueUrlResponse = await _amazonSqsClient.GetQueueUrlAsync(configuration.GetValue<string>("Amazon:SQSQueueName"), cancellationToken);
        
        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MessageBody = messageBody,
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageAttribute", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = operationName
                    }
                }
            }
        };
        
        var response = await _amazonSqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);

        return response;
    }
}