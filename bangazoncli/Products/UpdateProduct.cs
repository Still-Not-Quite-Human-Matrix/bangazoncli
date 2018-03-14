using System;
using System.Configuration;
using System.Collections.Generic;
using bangazoncli.Models;
using System.Data;
using System.Data.SqlClient;

namespace bangazoncli.Products
{
    class UpdateProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public bool productUpdater(string name, string price, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE [dbo].[Product]
                                    SET [Name] = @Name
                                    ,[Price] = @Price
                                    WHERE ProductID = @prodId";


                var NameParam = new SqlParameter("@Name", SqlDbType.NVarChar);
                NameParam.Value = name;
                cmd.Parameters.Add(NameParam);

                var PriceParam = new SqlParameter("@Price", SqlDbType.Money);
                PriceParam.Value = price;
                cmd.Parameters.Add(PriceParam);

                var productIdParam = new SqlParameter("@prodId", SqlDbType.Int);
                productIdParam.Value = id;
                cmd.Parameters.Add(productIdParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }

    class GetProductById
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public List<Product> GetProduct(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select *
                                    from Product
                                    where ProductID = @prodId";

                var productIdParam = new SqlParameter("@prodId", SqlDbType.Int);
                productIdParam.Value = id;
                cmd.Parameters.Add(productIdParam);

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

    public class ChangeProduct
    {
        public static bool ProductUpdater(int id)
        {
            //get product by id from selection
            var productSelector = new GetProductById();
            productSelector.GetProduct(id);


            View productFields = new View();
            Console.Clear();
            foreach (var field in productSelector.GetProduct(id))
            {
                Console.WriteLine($"1. Change Name: {field.Name}");
                Console.WriteLine($"2. Change Price: {field.Price}");
                Console.WriteLine($"3. Change Price: {field.Description}");
                Console.WriteLine($"4. Change Price: {field.Count}");
            }
            Console.ReadLine();




            //update selected product accordingly
            //var productQuery = new UpdateProduct();

            //Console.WriteLine("Please type the new product name:");
            //var productName = Console.ReadLine();

            //Console.WriteLine($"How much is {productName}:");
            //var price = Console.ReadLine();

            //var result = productQuery.productUpdater(productName, price, id);

            //Console.WriteLine("Type [0] to return to the main menu.");

            return true;
        }
    }
}

