using bangazoncli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli.OrderItems
{
    class RemoveOrderItemsFromList
    {
        public List<Product> RemoveFromListOfItems(List<Product> productData)
        {
            View ListView = new View();

            ListView.AddMenuText("Choose item to remove from your cart.");

            foreach (var product in productData)
            {
                ListView.AddMenuOption($"{product.Name}");
            }
            ListView.AddMenuText("0. Return to Main Menu");

            Console.Write(ListView.GetFullMenu());
            string userOption = Console.ReadLine();

            if (productData.Contains(productData[int.Parse(userOption)-1]))
            {
                var removed = productData.Remove(productData[int.Parse(userOption)-1]);
            }

            return productData;

        }
    }
}
