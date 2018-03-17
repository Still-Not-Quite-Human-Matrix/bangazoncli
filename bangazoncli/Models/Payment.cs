using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangazoncli.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string PaymentType { get; set; }
        public int PaymentAccountNum { get; set; }
        public int CustomerID { get; internal set; }
    }
}
