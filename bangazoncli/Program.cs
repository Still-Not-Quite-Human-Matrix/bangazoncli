using bangazoncli.Customers;
using bangazoncli.Products;
using System;
using cki = System.ConsoleKeyInfo;

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
                    case '0':
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
                        var ProductDataQuery = new GetProductData();

                        var productData = ProductDataQuery.getProducts();

                        

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
                .AddMenuOption("Add product to shopping cart")
                //.AddMenuOption("Complete an order")
                //.AddMenuOption("Remove customer product")
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

