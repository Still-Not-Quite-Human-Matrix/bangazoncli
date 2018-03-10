using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli
{
    internal class View
    {
        string companyName = @"
             __. ,  ..  .  .    ,   .__.     ,      .  .              
            (__ -+-*||  |\ | _ -+-  |  |. .*-+- _   |__|. .._ _  _.._ 
            .__) | |||  | \|(_) |   |__\(_|| | (/,  |  |(_|[ | )(_][ )
        ";


        

        IList<string> _menuItems;
        int itemNumber = 0;

        internal View()
        {
            _menuItems = new List<string> {companyName};
        }

        internal View AddMenuOption(string menuItem)
        {
            ++itemNumber;
            var menuEntry = $"{itemNumber}. {menuItem}";
            _menuItems.Add(menuEntry);
            return this;
        }

        internal string GetFullMenu()
        {
            Console.Clear();
            var menu = string.Join(Environment.NewLine, _menuItems);
            return $"{menu}{Environment.NewLine}> ";
        }

    }
}
