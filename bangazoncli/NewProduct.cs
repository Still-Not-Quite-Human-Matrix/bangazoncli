using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace bangazoncli
{
    class NewProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public bool InsertProduct(string name, string price)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Product 
                                (Name,
                                 Price)
                                VALUES (@Name, @Price)";


                var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
                NameParam.Value = name;
                cmd.Parameters.Add(NameParam);

                var PriceParam = new SqlParameter("@Price", System.Data.SqlDbType.Money);
                PriceParam.Value = price;
                cmd.Parameters.Add(PriceParam);


                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }
}
