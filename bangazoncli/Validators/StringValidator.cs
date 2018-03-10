using System;
using System.Text.RegularExpressions;
namespace bangazoncli.Validators
{
    public class StringValidator
    {
        public static void Main()
        {
            string[] customerStrings = { "" };
            Regex rgx = new Regex(@"^[A-Z][a-zA-Z]*$");
            foreach (string customerString in customerStrings)
                Console.WriteLine("{0} {1} a valid input.",
                                  customerString,
                                  rgx.IsMatch(customerString) ? "is" : "is not");
        }
    }
}



