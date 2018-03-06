using bangazoncli.Customers;
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
            var CustomerDataQuery = new GetCustomerData();

            var CustomerData = CustomerDataQuery.GetCustomerByName();

            foreach (var customer in CustomerData)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            }
            Console.ReadKey();
        }
    }
}
