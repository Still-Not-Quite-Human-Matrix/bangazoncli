using bangazoncli.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace bangazoncli.Products
{
    class GetProductData
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public List<Product> getProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT [ProductID], [Name], [Price]
                                        FROM [dbo].[Product]";

                var reader = cmd.ExecuteReader();

                List<Product> products = new List<Product>();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        Name = reader["Name"].ToString(),
                        Price = double.Parse(reader["price"].ToString()),
                        ProductID = int.Parse(reader["ProductID"].ToString())
                    };

                    products.Add(product);
                }

                return products;
            }
        }
    }
}
