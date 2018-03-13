using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace bangazoncli
{
    class CreatePaymentOption
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public bool InsertPaymentOption(string paymentType, int paymentAccountNum)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Payment 
                                (PaymentType,
                                 PaymentAccountNum)
                                VALUES (@PaymentType, @PaymentAccountNum)";

                var PaymentTypeParam = new SqlParameter("@PaymentType", System.Data.SqlDbType.NVarChar);
                PaymentTypeParam.Value = paymentType;
                cmd.Parameters.Add(PaymentTypeParam);

                var PaymentAccountNumParam = new SqlParameter("@PaymentAccountNum", System.Data.SqlDbType.Int);
                PaymentAccountNumParam.Value = paymentAccountNum;
                cmd.Parameters.Add(PaymentAccountNumParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }
}
