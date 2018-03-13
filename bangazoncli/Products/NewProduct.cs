using System;
using System.Data.SqlClient;
using System.Configuration;


namespace bangazoncli
{
    class NewProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public bool InsertProduct(string name, string price, int owner)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Product 
                                (Name,
                                 Price,
                                 Owner)
                                VALUES (@Name, @Price, @Owner)";


                var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
                NameParam.Value = name;
                cmd.Parameters.Add(NameParam);

                var PriceParam = new SqlParameter("@Price", System.Data.SqlDbType.Money);
                PriceParam.Value = price;
                cmd.Parameters.Add(PriceParam);

                var OwnerParam = new SqlParameter("@Owner", System.Data.SqlDbType.Int);
                OwnerParam.Value = owner;
                cmd.Parameters.Add(OwnerParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }

    public class ProductMaker
    {
        public static bool ProductCreator(int owner)
        {
            var productQuery = new NewProduct();

            Console.WriteLine("Please type product name:");
            var productName = Console.ReadLine();

            Console.WriteLine($"How much is {productName}:");
            var price = Console.ReadLine();

            var result = productQuery.InsertProduct(productName, price, owner);

            Console.WriteLine("Type [0] to return to the main menu.");

            return result;
        }
    }
}
