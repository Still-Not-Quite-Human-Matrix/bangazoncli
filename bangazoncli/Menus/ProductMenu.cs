using bangazoncli.Models;
using bangazoncli.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli.Menus
{
    class ProductMenus
    {
        public bool UpdateProduct(Customer activeCustomer)
        {
            Console.Clear();

            Console.WriteLine("Type a product id and press enter...");
            // Generate Product Menu //
            var customerProduct = new GetProductList();
            var products = customerProduct.GetProducts(activeCustomer.CustomerID);

            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID}. {product.Name}: {product.Price}");
            }
            Console.WriteLine("\nPress [0] to return to the main menu");

            // Read Input and Remove Product by ID // 
            var id = int.Parse(Console.ReadLine());
            if (id == 0)
                return true;

            if (ChangeProduct.ProductUpdater(id))
            {
                Console.WriteLine("Product updated. Press Enter to Continue");
                Console.ReadLine();
                return true;
            }
            else
            {
                Console.WriteLine("Product did not update");
                return false;
            }
        }

        public bool DeleteProduct(Customer activeCustomer)
        {
            Console.Clear();
            Console.WriteLine("1: Remove Product");
            Console.WriteLine("2: Return to Main Menu");
            var productSubSelection = Console.ReadKey();
            var remove = true;
            while (remove)
            {
                switch (productSubSelection.KeyChar)
                {
                    case '0':
                        remove = false;
                        break;

                    case '1':
                        Console.Clear();
                        Console.WriteLine("Type a product id and press enter...");
                        // Generate Product Menu //
                        var customerProducts = new GetProductList();
                        var ProductData = customerProducts.GetProducts(activeCustomer.CustomerID);

                        foreach (var product in ProductData)
                        {
                            Console.WriteLine($"{product.ProductID}. {product.Name}: {product.Price}");
                        }
                        Console.WriteLine("\nPress [0] to return to the main menu");

                        // Read Input and Remove Product by ID // 
                        var selection = Console.ReadLine();
                        var productDelete = new RemoveProduct().DeleteProduct(int.Parse(selection));
                        if (int.Parse(selection) == 0)
                        {
                            remove = false;
                            
                        }
                        else if (productDelete)
                        {
                            Console.WriteLine("Product deleted press enter to relaod list");
                            Console.ReadKey();    
                        }
                        else
                        {
                            Console.WriteLine("Product not deleted or does not exist");
                        }
                        //Console.ReadKey();
                        break;

                    case '2':
                        remove = false;
                        break;
                }
            }
            return true;
        }
    }
}
