using CheckPrices.Domain.Contracts;
using CheckPrices.Domain.Domain;
using CheckPrices.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPrices.Application.UseCase
{
    public class GetProductsUseCase : IGetProductsUseCase
    {
        public IProductRepository productRepository;
        public GetProductsUseCase(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync() 
        {
            IEnumerable<Product> products = await productRepository.GetProducts();

            return products;
        }
    }
}
