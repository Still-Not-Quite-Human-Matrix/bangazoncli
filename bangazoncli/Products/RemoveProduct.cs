using System.Configuration;
using System.Data.SqlClient;

namespace bangazoncli
{
    class RemoveProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;

        public bool DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Delete
                                    From Product
                                    Where ProductId = @ProductId";

                var productIdParam = new SqlParameter("@ProductId", System.Data.SqlDbType.Int);

                productIdParam.Value = productId;
                cmd.Parameters.Add(productIdParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result >= 1;
            }
        }
    }
}
