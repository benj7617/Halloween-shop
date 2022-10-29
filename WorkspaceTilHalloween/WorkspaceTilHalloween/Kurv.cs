using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceTilHalloween
{
    internal class Kurv
    {
        public string productName { get; set; }
        public int productPrice { get; set; }
        public int productID { get; set; }

        public Kurv (string productName, int productPrice, int productID)
        {
            this.productName = productName;
            this.productPrice = productPrice;
            this.productID = productID;
        }
    }
}
