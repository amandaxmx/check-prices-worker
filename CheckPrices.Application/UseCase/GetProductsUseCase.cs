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

        public async Task<IEnumerable<Product>> ExecuteAsync() 
        {
            var products = await _productRepository.GetProducts();
            var updatedProducts = new List<Product>();

            foreach (var product in products)
            {
                if (string.IsNullOrWhiteSpace("product.Url"))
                {
                    updatedProducts.Add(product);
                    continue;
                }

                var newPrice = await _priceCheckerService.GetPriceFromUrlAsync(product.Url);

                if (newPrice.HasValue && newPrice.Value != product.Price)
                {
                    var updatedProduct = new Product(product.Name, product.Url, newPrice.Value, product.Active);
                    updatedProduct.SetId(product.Id);
                    updatedProducts.Add(updatedProduct);
                }
                else
                {
                    updatedProducts.Add(product);
                }
            }

            return updatedProducts;
        }
    }
}
