using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MYB.DAO
{
    public class OrderDetails
    {
        public OrderDetails() { }

        public string proName { get; set; }

        public int quality { get; set; }

        public float price { get; set; }
        public float subTotal { get; set; }
        public string proID { get; set; }
        public string ordID { get; set; }
    }
}
