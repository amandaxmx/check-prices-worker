using Amazon.SQS;
using Amazon.SQS.Model;
using CheckPrices.Application.UseCase;
using CheckPrices.Domain.Domain;
using CheckPrices.Domain.Model;
using System.Text.Json;

public class Worker
{
    private readonly IGetProductsUseCase _getProductsUseCase;
    private readonly IAmazonSQS _sqsClient;

    public Worker(IGetProductsUseCase getProductsUseCase, IAmazonSQS sqsClient)
    {
        _getProductsUseCase = getProductsUseCase;
        _sqsClient = sqsClient;
    }

    public async Task RunAsync()
    {
        Product product = await _getProductsUseCase.ExecuteAsync();

        if (product != null)
        {
            var productJson = JsonSerializer.Serialize(product);

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = "http://localhost:4566/000000000000/minha-fila-teste",
                MessageBody = productJson
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }
}
