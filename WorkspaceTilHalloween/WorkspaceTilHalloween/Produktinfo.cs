using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceTilHalloween
{
    internal class Produktinfo
    {
        public int productID { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int numberInStock { get; set; }
        public string description { get; set; }
        public string category { get; set; }

        public Produktinfo(int productID, string name, int price, int numberInStock, string description, string category)
        {
            this.productID = productID;
            this.name = name;
            this.price = price;
            this.numberInStock = numberInStock;
            this.description = description;
            this.category = category;
        }
    }
}
