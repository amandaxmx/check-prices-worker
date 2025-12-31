using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPrices.Domain.Model
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Url { get; private set; }
        public decimal Price { get; private set; }
        public string Active { get; private set; }

        public Product(string name, string? url, decimal price, string active)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("O nome do produto não pode ser nulo ou vazio.", nameof(name));

            if (price < 0)
                throw new ArgumentException("O valor do produto não pode ser negativo.", nameof(price));

            Name = name;
            Url = url;
            Price = price;
            Active = active;
        }

        private Product() { }

        public void SetId(int id)
        {
            if (Id != 0)
                throw new InvalidOperationException("O Id do produto já foi definido.");
            if (id <= 0)
                throw new ArgumentException("O Id do produto deve ser um número positivo.", nameof(id));

            Id = id;
        }
    }
}
