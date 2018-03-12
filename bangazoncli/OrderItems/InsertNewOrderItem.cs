using System.Data.SqlClient;
using System.Configuration;

namespace bangazoncli
{
    class InsertNewOrderItem
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;


        public bool InsertOrderItem(int orderID, int productID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO OrderItem 
                                (OrderID, 
                                ProductID)
                                VALUES ( @ProductID)";

                var OrderIDParam = new SqlParameter("@OrderID", System.Data.SqlDbType.Int);
                OrderIDParam.Value = orderID;
                cmd.Parameters.Add(OrderIDParam);

                var ProductIDParam = new SqlParameter("@ProductID", System.Data.SqlDbType.Int);
                ProductIDParam.Value = productID;
                cmd.Parameters.Add(ProductIDParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }
}
