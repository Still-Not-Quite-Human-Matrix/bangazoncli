using bangazoncli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli.OrderItems
{
    class ShowOrderItems
    {
        public string ShowListOfItems(List<Product> productData)
        {
            View ListView = new View();

            ListView.AddMenuText("These are the items in your cart");

            foreach (var product in productData)
            {
                ListView.AddMenuText($"{product.Name}");
            }
            ListView.AddMenuText("0. Return to Main Menu");

            Console.Write(ListView.GetFullMenu());
            string userOption = Console.ReadLine();
            return userOption;

        }
    }
}
