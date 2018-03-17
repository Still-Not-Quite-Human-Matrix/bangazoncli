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

        public bool InsertPaymentOption(string paymentType, int paymentAccountNum, int activeCustomer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Payment 
                                (PaymentType,
                                 PaymentAccountNum,
                                 CusID)
                                VALUES (@PaymentType, @PaymentAccountNum, @CustomerID)";

                var PaymentTypeParam = new SqlParameter("@PaymentType", System.Data.SqlDbType.NVarChar);
                PaymentTypeParam.Value = paymentType;
                cmd.Parameters.Add(PaymentTypeParam);

                var PaymentAccountNumParam = new SqlParameter("@PaymentAccountNum", System.Data.SqlDbType.Int);
                PaymentAccountNumParam.Value = paymentAccountNum;
                cmd.Parameters.Add(PaymentAccountNumParam);

                var custIDParam = new SqlParameter("@CustomerID", System.Data.SqlDbType.Int);
                custIDParam.Value = activeCustomer;
                cmd.Parameters.Add(custIDParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }
    public class ThePaymentTypeCreator
    {
        public static bool PaymentCreator(int activeCustomer)
        {
            var paymentQuery = new CreatePaymentOption();

            Console.WriteLine("Enter Payment Type (Amex, Mastercard, Visa, Checking):");
            var paymentType = Console.ReadLine();

            Console.WriteLine("Enter account number:");
            var paymentAccountNum = int.Parse(Console.ReadLine());


            var result = paymentQuery.InsertPaymentOption(paymentType, paymentAccountNum, activeCustomer);

            Console.WriteLine("Type [0] to return to the main menu.");

            return result;
        }
    }

}
