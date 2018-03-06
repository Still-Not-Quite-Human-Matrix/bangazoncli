using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = SetupNewApp();

            var run = true;
            while (run)
            {
                ConsoleKeyInfo userInput = MainMenu();
            }
        }

        static DatabaseContext SetupNewApp()
        {
            Console.Title = "Bangazon Command Line Ordering System";
            var cSharp = 554;
            var db = new DatabaseContext(tone: cSharp);
            return db;
        }

        static ConsoleKeyInfo MainMenu()
        {
            View mainMenu = new View()
                .AddMenuOption("Create a customer account")
                .AddMenuOption("Choose active customer");
                //.AddMenuOption("Create a payment option")
                //.AddMenuOption("Add product to sell")
                //.AddMenuOption("Add product to shopping cart")
                //.AddMenuOption("Complete an order")
                //.AddMenuOption("Remove customer product")
                //.AddMenuOption("Update product information")
                //.AddMenuOption("Show stale products")
                //.AddMenuOption("Show customer revenue report")
                //.AddMenuOption("Show overall product popularity")
                //.AddMenuOption("Leave Bangazon!");

            Console.Write(mainMenu.GetFullMenu());
            ConsoleKeyInfo userOption = Console.ReadKey();
            return userOption;

        }
    }
}

