using bangazoncli.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
namespace bangazoncli.Orders
{
    class GetOrderData
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public Order GetOrderByCustomerID(int customerID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Select [PaymentID], [CustomerID], [OrderID]
                                        From [dbo].[Order]
                                    Where CustomerID = @CustomerID";

                var CustomerIDParam = new SqlParameter("@CustomerID", System.Data.SqlDbType.Int);
                CustomerIDParam.Value = customerID;
                cmd.Parameters.Add(CustomerIDParam);


                var reader = cmd.ExecuteReader();

                Order customerOrder = new Order();

                while (reader.Read())
                {
                    var order = new Order
                    {
                        OrderID = int.Parse(reader["OrderID"].ToString()),
                        PaymentID = int.Parse(reader["PaymentID"].ToString()),
                        CustomerID = int.Parse(reader["CustomerID"].ToString())
                    };

                    customerOrder = order;
                }

                return customerOrder;
            }
        }
    }
}


