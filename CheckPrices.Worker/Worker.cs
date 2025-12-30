using CheckPrices.Application.UseCase;
using CheckPrices.Domain.Domain;

public class Worker
{
    private readonly IGetProductsUseCase _getProductsUseCase;

    public Worker(IGetProductsUseCase getProductsUseCase)
    {
        _getProductsUseCase = getProductsUseCase;
    }

    public async Task RunAsync()
    {
        await _getProductsUseCase.ExecuteAsync();
    }
}
