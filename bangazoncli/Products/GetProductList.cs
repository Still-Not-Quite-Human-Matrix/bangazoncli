using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using bangazoncli.Models;

namespace bangazoncli.Products
{
    class GetProductList
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        //this will need to be a get products by customer query at this time it returns all products in the DB
        public List<Product> GetProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select *
                                    from Product";

                var reader = cmd.ExecuteReader();

                var products = new List<Product>();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = int.Parse(reader["ProductId"].ToString()),
                        ProductName = reader["Name"].ToString(),
                        ProductPrice = double.Parse(reader["Price"].ToString()),
                    };

                    products.Add(product);
                }

                return products;
            }
        }
    }
}
