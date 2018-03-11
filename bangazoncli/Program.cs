using bangazoncli.Customers;
using bangazoncli.Products;
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

            Customer activeCustomer = null;


            var run = true;
            while (run)
            {
                cki userInput = MainMenu(activeCustomer);


                switch (userInput.KeyChar)
                {
                    case '0':
                        run = false;
                        break;

                    case '1':
                        Console.Clear();

                        var customerQuery = new CreateNewCustomer();
                        var result = customerQuery.InsertCustomer("john", "doe", "1st street", "nashville", "TN", "37064", "5555555555");

                        if (result)
                        {
                            Console.WriteLine("Customer added successfully.");
                        }

                        Console.ReadLine();

                        break;

                    case '2':
                        Console.Clear();

                        var customerDataQuery = new GetCustomerData();
                        var customerData = customerDataQuery.GetCustomerByName();

                        var chosenCustomer = int.Parse(ChooseActiveCustomerMenu(customerData));

                        if (chosenCustomer != 0 && chosenCustomer < customerData.Count + 1)
                        {
                            activeCustomer = customerData[chosenCustomer - 1];
                        }

                        break;

                    case '3':
                        var runThisMenu = true;

                        if (activeCustomer != null)
                        {

                            while (runThisMenu)
                            {

                                var productDataQuery = new GetProductData();
                                var productData = productDataQuery.getProducts();

                                var chosenInput = int.Parse(ChooseProductMenu(productData));

                                if (chosenInput != 0)
                                {
                                    var itemToAdd = SetupOrderItem(chosenInput, productData, activeCustomer);

                                    var createOrderItem = new CreateNewOrderItem();

                                    var orderItemResult = createOrderItem.InsertOrderItem(itemToAdd.OrderID, itemToAdd.ProductID);

                                    Console.WriteLine($"Item {itemToAdd.ProductID} successfully added to {activeCustomer.FirstName} {activeCustomer.LastName}'s order");
                                }
                                else
                                {
                                    runThisMenu = false;
                                }

                            }
                        }

                        break;

                    case '4':

                        Console.Clear();
                        var productQuery = new NewProduct();
                        var productResult = productQuery.InsertProduct("Shoe", "$40");

                        if (productResult)
                        {
                            Console.WriteLine("Product added successfully.");
                        }

                        Console.ReadLine();
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
                .AddMenuOption("(Under Construction, Only displays products) Add product to shopping cart")
                .AddMenuOption("Add product to sell")
                //.AddMenuOption("Complete an order")
                //.AddMenuOption("Remove customer product")
                //.AddMenuOption("Update product information")
                //.AddMenuOption("Show stale products")
                //.AddMenuOption("Show customer revenue report")
                //.AddMenuOption("Show overall product popularity")
                .AddMenuText("Press [0] To Leave Bangazon!");


            Console.Write(mainMenu.GetFullMenu());

            if (activeCustomer != null)
            {
                Console.WriteLine($"Your current active Customer is {activeCustomer.FirstName} {activeCustomer.LastName}");
            } else
            {
                Console.WriteLine("No active customer set");
            }

            cki userOption = Console.ReadKey();
            return userOption;

        }

        static string ChooseActiveCustomerMenu(List<Customer> customerData)
        {
            View ChooseMenu = new View().AddMenuText("Which customer will be active?");

            foreach (var customer in customerData)
            {
                ChooseMenu.AddMenuOption($"Customer ID: {customer.CustomerID} Name: {customer.FirstName} {customer.LastName}");
            }
            ChooseMenu.AddMenuText("0. Return to Main Menu");

            Console.Write(ChooseMenu.GetFullMenu());
            string userOption = Console.ReadLine();
            return userOption;

        }

        static string ChooseProductMenu(List<Product> productData)
        {

            View ProductsView = new View().AddMenuText("These are all available products.");

            foreach (var product in productData)
            {
                ProductsView.AddMenuOption($"Product ID: {product.ProductID}, Name: {product.Name}, Price: {product.Price}");
            }

            ProductsView.AddMenuText("0. Done adding products");

            Console.Write(ProductsView.GetFullMenu());
            string userOption = Console.ReadLine();

            return userOption;
        }

        
        static OrderItem SetupOrderItem(int userInput, List<Product> productData, Customer activeCustomer)
        {

            /* If an order with a matching CustomerID exists 
             * then add this item to that order
             * 
             * Else
             * insert new Order
             * leave an open spot for paymentID
             * add activeCustomer.CustomerID
             * Use this new OrderID when creating an orderItem
            */


            var chosenProduct = new OrderItem
            {
                ProductID = productData[userInput - 1].ProductID,
                //OrderID = 
            };

            return chosenProduct;
        }
    }
}

