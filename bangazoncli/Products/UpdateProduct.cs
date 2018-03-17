using System;
using System.Configuration;
using System.Collections.Generic;
using bangazoncli.Models;
using System.Data;
using System.Data.SqlClient;
using cki = System.ConsoleKeyInfo;

namespace bangazoncli.Products
{
    class UpdateProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public bool productUpdater(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE [dbo].[Product]
                                    SET [Name] = @Name
                                    ,[Price] = @Price
                                    ,[Description] = @prodDesc
                                    ,[Count] = @prodCount
                                    WHERE ProductID = @prodId";


                var NameParam = new SqlParameter("@Name", SqlDbType.NVarChar);
                NameParam.Value = product.Name;
                cmd.Parameters.Add(NameParam);

                var PriceParam = new SqlParameter("@Price", SqlDbType.Money);
                PriceParam.Value = product.Price;
                cmd.Parameters.Add(PriceParam);

                var productIdParam = new SqlParameter("@prodId", SqlDbType.Int);
                productIdParam.Value = product.ProductID;
                cmd.Parameters.Add(productIdParam);

                var productDescParam = new SqlParameter("@prodDesc", SqlDbType.NVarChar);
                productDescParam.Value = product.Description;
                cmd.Parameters.Add(productDescParam);

                var productCountParam = new SqlParameter("@prodCount", SqlDbType.Int);
                productCountParam.Value = product.Count;
                cmd.Parameters.Add(productCountParam);

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
                cmd.CommandText = @"select [ProductID], [Name], [Price], [Count], [Description]
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
                        Count = int.Parse(reader["Count"].ToString()),
                        Description = reader["Description"].ToString()
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
            
            //create a new product
            Product newProduct = new Product();
            foreach (var field in productSelector.GetProduct(id))
            {
                newProduct.ProductID = field.ProductID;
                newProduct.Name = field.Name;
                newProduct.Price = field.Price;
                newProduct.Count = field.Count;
                newProduct.Description = field.Description;
            }

            cki input;

            Console.Clear();
            cki ProductSubMenu()
            { 
                View productSubMenu = new View();
                {
                    productSubMenu
                        .AddMenuOption($"Change Name: {newProduct.Name}")
                        .AddMenuOption($"Change Price: {newProduct.Price}")
                        .AddMenuOption($"Change Description: {newProduct.Description}")
                        .AddMenuOption($"Change Count: {newProduct.Count}");
                        //.AddMenuOption("Enter [0] to end editing, save changes, and return to main menu");
                }
                Console.Write(productSubMenu.GetFullMenu());
                Console.WriteLine("Press [0] to save changes and return to main menu");
                input = Console.ReadKey();
                return input;
            }

            var productQuery = new UpdateProduct();
            var update = true; 
            while (update)
            {
                ProductSubMenu();
                switch (input.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Please type the new product name:");
                        newProduct.Name = Console.ReadLine();
                        break;
                    case '2':
                        Console.WriteLine($"Updated Price:");
                        newProduct.Price = double.Parse(Console.ReadLine());
                        break;
                    case '3':
                        Console.WriteLine($"Update Description:");
                        newProduct.Description = Console.ReadLine();
                        break;
                    case '4':
                        Console.WriteLine($"Update Count:");
                        newProduct.Count = int.Parse(Console.ReadLine());
                        break;
                    case '0':
                        var result = productQuery.productUpdater(newProduct);
                        update = false;
                        break;         
                } 
            }
            return true;
        }
    }
}

