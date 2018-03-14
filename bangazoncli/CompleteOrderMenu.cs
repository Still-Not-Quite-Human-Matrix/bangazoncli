using bangazoncli.Models;
using bangazoncli.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli
{
    class CompleteOrderMenu
    {
        internal bool CompleteOrder(List<Product> listOfOrderItems, Customer activeCustomer)
        {
            //choose customer payment option.
            //when option is chosen it should be added to the open order.

            var total = 0.0;

            foreach (var product in listOfOrderItems)
            {
                total += product.Price;
            }

            Console.WriteLine($"Your order Total is {total} Ready to Purchase? (Y/N)");

            var userInput = (Console.ReadLine()).ToUpper();

            if (userInput == "Y")
            {
                var paymentData = new GetPaymentData();
                var customerPaymentData = paymentData.GetPaymentsByCustomerID(activeCustomer.CustomerID);


                View menu = new View();

                foreach( var payment in customerPaymentData)
                {
                    menu.AddMenuOption(payment.PaymentType);
                }
            }

            

            return true;




            
        }
    }
}
