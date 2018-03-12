using bangazoncli.Customers;
using bangazoncli.Products;
using bangazoncli.Orders;
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

                                // Show the user all the products in the store
                                var productDataQuery = new GetProductData();
                                var productData = productDataQuery.getProducts();

                                // When the user chooses from the menu, make it an int instead of a string (from console.readline)
                                var chosenInput = int.Parse(ChooseProductMenu(productData));

                                // 0 is the option to return to the main menu, so if anything else was chosen...
                                if (chosenInput != 0)
                                {

                                    // Send the user's option as an int, the list of products, and the user's info along to be made into an OrderItem
                                    // We need the user's info to see their Order
                                    // this will return the formatted OrderItem, not yet Inserted
                                    var orderItemToAdd = SetupOrderItem(chosenInput, productData, activeCustomer);

                                    // Now we make complete the insert, sending the formatted OrderItem we made above
                                    var orderInsert = new InsertNewOrderItem();
                                    var orderItemResult = orderInsert.InsertOrderItem(orderItemToAdd.OrderID, orderItemToAdd.ProductID);

                                    // When it inserts successfully or not we'll tell the user they did it or didn't do it
                                    if (orderItemResult)
                                    {
                                        Console.WriteLine($"Item {orderItemToAdd.ProductID} successfully added to {activeCustomer.FirstName} {activeCustomer.LastName}'s order");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Could not add product");
                                    }
                                }

                                // The above menu will live on until the user hits 0 to leave this menu
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
            // First, we need to see if the user has an Order with us already (we are only allowing one order per customer here)
            var customerOrdersData = new GetOrderData();
            var customerOrder = customerOrdersData.GetOrderByCustomerID(activeCustomer.CustomerID);

            // In the case they do not have an order with us...
            if (customerOrder == null)
            {
                // We will create new order
                // Hard code a PaymentID for now
                // Then set this new order to customerOrder. Doing this we set the variable that was 'null' to an actual order, so we can proceed.
            }

            // At this point we know the customer has an order so we can continue to add orderItem to that order

            // The function that's calling this one will be adding this OrderItem to our DB.
            // Which means what we have to do here is add the ProductID, so we know what they're buying, and the OrderID, so we know what they have in their 'cart'
            var chosenProduct = new OrderItem
            {
                ProductID = productData[userInput - 1].ProductID,
                OrderID = customerOrder.OrderID
            };




            // Now this chosenProduct is an OrderItem that will carry the product and order ID's along
            return chosenProduct;
        }
    }
}

