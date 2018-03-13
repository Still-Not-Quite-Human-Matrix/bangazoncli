using bangazoncli.Models;
using bangazoncli.Orders;
using System.Collections.Generic;

namespace bangazoncli.OrderItems
{
    class CreateNewOrderItem
    {
        public OrderItem SetupOrderItem(int userInput, List<Product> productData, Customer activeCustomer)
        {
            var customerOrderData = new GetOrderData();
            var customerOrder = customerOrderData.GetOrderByCustomerID(activeCustomer.CustomerID);

            var chosenProduct = new OrderItem
            {
                ProductID = productData[userInput - 1].ProductID,
                OrderID = customerOrder.OrderID
            };

            return chosenProduct;
        }
    }
}
