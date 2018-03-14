using bangazoncli.Models;
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

            Console.WriteLine($"Your order Total is {total} Ready to Purchase?");

            View menu = new View()
                .AddMenuOption("Visa")
                .AddMenuOption("Amex");

            return true;




            
        }
    }
}
