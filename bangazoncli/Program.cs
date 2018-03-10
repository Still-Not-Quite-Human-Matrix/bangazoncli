using bangazoncli.Customers;
using bangazoncli.Models;
using System;
using System.Collections.Generic;
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


            var productQuery = new NewProduct();
            var productResult = productQuery.InsertProduct("Shoe", "$40");

            Customer activeCustomer = null;


            var run = true;
            while (run)
            {
                cki userInput = MainMenu(activeCustomer);


                switch (userInput.KeyChar)
                {
                    case '3':
                        run = false;
                        break;


                    case '2':
                        Console.Clear();

                        var customerDataQuery = new GetCustomerData();
                        var customerData = customerDataQuery.GetCustomerByName();

                        var chosenCustomer = int.Parse(ChooseActiveCustomerMenu(customerData).KeyChar.ToString());

                        activeCustomer = customerData[chosenCustomer - 1];

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

        static cki MainMenu(Customer activeCustomer)
        {

            View mainMenu = new View()
                .AddMenuOption("Create a customer account")
                .AddMenuOption("Choose active customer")
                //.AddMenuOption("Create a payment option")
                //.AddMenuOption("Add product to sell")
                //.AddMenuOption("Add product to shopping cart")
                //.AddMenuOption("Complete an order")
                //.AddMenuOption("Remove customer product")
                //.AddMenuOption("Update product information")
                //.AddMenuOption("Show stale products")
                //.AddMenuOption("Show customer revenue report")
                //.AddMenuOption("Show overall product popularity")
                .AddMenuOption("Leave Bangazon!");


            Console.Write(mainMenu.GetFullMenu());

            if (activeCustomer != null)
            {
                Console.WriteLine($"Your current active Customer is {activeCustomer.FirstName} {activeCustomer.LastName}");
            }

            cki userOption = Console.ReadKey();
            return userOption;

        }

        static cki ChooseActiveCustomerMenu(List<Customer> CustomerData)
        {
            View ChooseMenu = new View().AddMenuText("Which customer will be active?");

            foreach (var customer in CustomerData)
            {
                ChooseMenu.AddMenuOption($"Customer ID: {customer.CustomerID} Name: {customer.FirstName} {customer.LastName}");
            }
            ChooseMenu.AddMenuOption("Exit!");

            Console.Write(ChooseMenu.GetFullMenu());
            cki userOption = Console.ReadKey();
            return userOption;

        }
    }
}

