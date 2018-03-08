using bangazoncli.Customers;
using System;
using bangazoncli.Products;
using cki = System.ConsoleKeyInfo;
using bangazoncli.Models;
using System.Collections.Generic;

namespace bangazoncli
{
    class Program

    {
        static void Main(string[] args)
        {

            var db = SetupNewApp();
            var customerQuery = new CreateNewCustomer();
            var result = customerQuery.InsertCustomer("john", "doe", "1st street", "nashville", "TN", "37064", "5555555555");

            var run = true;
            while (run)
            {
                cki userInput = MainMenu();

                switch (userInput.KeyChar)
                {
                    case '4':
                        run = false;
                        break;


                    case '2':
                        Console.Clear();

                        var CustomerDataQuery = new GetCustomerData();

                        var CustomerData = CustomerDataQuery.GetCustomerByName();

                        foreach (var customer in CustomerData)
                        {
                            Console.WriteLine($"{customer.FirstName} {customer.LastName}");
                        }
                        Console.ReadKey();
                        break;

                    case '3':
                        Console.Clear();
                        Console.WriteLine("1: Remove Product");
                        Console.WriteLine("2: Return to Main Menu");
                        cki productSubSelection = Console.ReadKey();
                        var remove = true;
                        while (remove)
                        { 
                            switch (productSubSelection.KeyChar)
                            {
                                case '1':
                                    Console.Clear();
                                    Console.WriteLine("Type a product id and press enter...");
                                    // Generate Product Menu //
                                    var productQuery = new GetProductList();
                                    var ProductData = productQuery.GetProducts();

                                    foreach (var product in ProductData)
                                    {
                                        Console.WriteLine($"{product.ProductId}. {product.ProductName}: {product.ProductPrice}");
                                    }

                                    // Read Input and Remove Product by ID // 
                                    var selection = Console.ReadLine();
                                    var productDelete = new RemoveProduct().DeleteProduct(int.Parse(selection));
                                    if (productDelete)
                                    {
                                        Console.WriteLine("Product deleted press esc to relaod list");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Product not deleted or does not exist");
                                    }
                                    Console.ReadKey();
                                    break;
                                case '2':
                                    remove = false;
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        static DatabaseContext SetupNewApp()
        {
            Console.Title = "Bangazon Command Line Ordering System";
            var db = new DatabaseContext();
            return db;
        }

        static cki MainMenu()
        {
            View mainMenu = new View()
                .AddMenuOption("Create a customer account")
                .AddMenuOption("Choose active customer")
                //.AddMenuOption("Create a payment option")
                //.AddMenuOption("Add product to sell")
                //.AddMenuOption("Add product to shopping cart")
                //.AddMenuOption("Complete an order")
                .AddMenuOption("Remove customer product")
                //.AddMenuOption("Update product information")
                //.AddMenuOption("Show stale products")
                //.AddMenuOption("Show customer revenue report")
                //.AddMenuOption("Show overall product popularity")
                .AddMenuOption("Leave Bangazon!");

            Console.Write(mainMenu.GetFullMenu());
            cki userOption = Console.ReadKey();
            return userOption;

        }
    }
}

