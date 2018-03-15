using bangazoncli.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli.Payments
{
    class GetPaymentData
    {

        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public List<Payment> GetPaymentsByCustomerID(int CustomerID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Select [PaymentID], [PaymentType], [PaymentAccountNum], [CustomerID]
                                From [dbo].[Payment]";

                var reader = cmd.ExecuteReader();

                List<Payment> customerPayments = new List<Payment>();

                while (reader.Read())
                {
                    var payment = new Payment
                    {
                        PaymentID = int.Parse(reader["PaymentID"].ToString()),
                        PaymentType = reader["PaymentType"].ToString(),
                        PaymentAccountNum = int.Parse(reader["PaymentAccountNum"].ToString()),
                        CustomerID = int.Parse(reader["CustomerID"].ToString())
                    };

                    customerPayments.Add(payment);
                }

                return customerPayments;
            }
        }
    }
}
