using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using bangazoncli.Models;

namespace bangazoncli.Products
{
    class GetProductList
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public List<Product> GetProducts(int custID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select *
                                    from Product
                                    where Owner = @custID";

                var custIDParam = new SqlParameter("@custID", SqlDbType.NVarChar);
                custIDParam.Value = custID;
                cmd.Parameters.Add(custIDParam);

                var reader = cmd.ExecuteReader();

                var products = new List<Product>();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductID = int.Parse(reader["ProductId"].ToString()),
                        Name = reader["Name"].ToString(),
                        Price = double.Parse(reader["Price"].ToString()),
                    };

                    products.Add(product);
                }

                return products;
            }
        }
        
    }
}