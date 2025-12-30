using CheckPrices.Application.UseCase;
using CheckPrices.Domain.Contracts;
using CheckPrices.Domain.Domain;
using CheckPrices.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IGetProductsUseCase, GetProductsUseCase>();
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<Worker>();
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var worker = scope.ServiceProvider.GetRequiredService<Worker>();

        await worker.RunAsync();
    }
}
