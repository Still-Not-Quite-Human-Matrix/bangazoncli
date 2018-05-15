using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace bangazoncli.Products
{
    class ProductPopularity
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public bool popularProducts (_________________)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"";

            }

            public class TheProductPopularityGenerator
            {
                public static bool ProductPopularityInfo()
                {
                    var popularityQuery = new ProductPopularity();

                    Console.WriteLine("****************************************************************");
                    Console.WriteLine("Products             | Orders      |   Purchasers    | Revenue");
                    Console.WriteLine("****************************************************************");
                    Console.WriteLine(String.Format("{0,-20} | {1,-11} | {2,-15} | {3, -15}", "Adam's Mac Book", "", "", ""));
                    Console.WriteLine(String.Format("{0,-20} | {1,-11} | {2,-15} | {3, -15}", "Alex's soul", "", "", ""));
                    Console.WriteLine(String.Format("{0,-20} | {1,-11} | {2,-15} | {3, -15}", "Slightly used tire", "", "", ""));
                    Console.WriteLine("****************************************************************");
                    Console.WriteLine(String.Format("{0,-20}  {1,-11}  {2,-15}  {3, -15}", "Totals", "", "", ""));


            }
            }


    }
}
