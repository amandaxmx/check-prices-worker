using CheckPrices.Domain.Contracts;
using CheckPrices.Domain.Domain;
using CheckPrices.Domain.Model;

namespace CheckPrices.Application.UseCase
{
    public class GetProductsUseCase : IGetProductsUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPriceCheckerService _priceCheckerService;

        public GetProductsUseCase(IProductRepository productRepository, IPriceCheckerService priceCheckerService) 
        {
            _productRepository = productRepository;
            _priceCheckerService = priceCheckerService;
        }

        public async Task<Product> ExecuteAsync() 
        {
            var products = await _productRepository.GetProducts();
            var updatedProducts = new List<Product>();

            foreach (var product in products)
            {
                if (string.IsNullOrWhiteSpace(product.Url))
                {
                    return null;
                }

                var newPrice = await _priceCheckerService.GetPriceFromUrlAsync(product.Url);

                if (newPrice.HasValue && newPrice.Value < product.Price)
                {
                    product.SetNewPrice(newPrice.Value, product);
                    return product;
                }

            }
            return null;
        }
    }
}
