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
        View ListView = new View();

        public string ShowListOfItems(List<Product> productData)
        {

            ListView.AddMenuText("These are the items in your cart");

            foreach (var product in productData)
            {
                ListView.AddMenuText($"{product.Name}, {product.Price}");
            }
            ListView.AddMenuText("0. Return to Main Menu");

            ShowTotal(productData);

            Console.Write(ListView.GetFullMenu());
            string userOption = Console.ReadLine();
            return userOption;

        }
        public void ShowTotal(List<Product> productData)
        {
            double total = 0;
            foreach (var item in productData)
            {
                total += item.Price;
            }

            ListView.AddMenuText($"Total: ${total}");
        } 
    }
}
