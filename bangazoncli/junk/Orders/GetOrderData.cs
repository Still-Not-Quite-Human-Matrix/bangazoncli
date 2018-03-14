using bangazoncli.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                cmd.CommandText = @"Select *
                                    From [dbo].[Order]
                                    Where CustomerID = @customerID";

                var customerIDParam = new SqlParameter("@customerID", System.Data.SqlDbType.Int);
                customerIDParam.Value = customerID;
                cmd.Parameters.Add(customerIDParam);
                var reader = cmd.ExecuteReader();

                var order = new Order();

                while (reader.Read())
                {
                    order.OrderID = int.Parse(reader["OrderID"].ToString());
                    order.PaymentID = int.Parse(reader["PaymentID"].ToString());
                    order.CustomerID = int.Parse(reader["CustomerID"].ToString());

                }

                return order;
            }
        }


    }
}
