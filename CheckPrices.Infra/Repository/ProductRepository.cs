using CheckPrices.Domain.Contracts;
using CheckPrices.Domain.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPrices.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Postgres")
                ?? throw new InvalidOperationException("Connection string não encontrada");
        }


        public async Task<IEnumerable<Product>> GetProducts()
        {
            const string sql = """
            SELECT * FROM product;
            """;

            using IDbConnection connection = new NpgsqlConnection(_connectionString);

            var products = await connection.QueryAsync<Product>(sql);
            return products;
        }
    }
}
