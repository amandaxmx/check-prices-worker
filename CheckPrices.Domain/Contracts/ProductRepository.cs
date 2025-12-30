using CheckPrices.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPrices.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}
