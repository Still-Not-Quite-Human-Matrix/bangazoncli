using bangazoncli.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace bangazoncli.Customers
{
    class GetCustomerData
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;
        
        public List<Customer> GetCustomerByName()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Select [FirstName], [LastName], [CustomerId]
                                    From [dbo].[Customer]";

                var reader = cmd.ExecuteReader();

                List<Customer> customers = new List<Customer>();

                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        CustomerID = int.Parse(reader["CustomerId"].ToString())
                    };

                    customers.Add(customer);
                }

                return customers;
            }
        }
    }
}
