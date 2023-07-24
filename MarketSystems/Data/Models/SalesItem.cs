using MarketSystems.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystems.Data.Models
{
    public class SalesItem : BaseEntity
    {

        private static int count = 0;

        public SalesItem(int quantity, string saleproducts)
        {
            Quantity = quantity;
            SaleProducts = saleproducts;

            Id = count;
            count++;
        }
        public int Quantity { get; set; }
        public string SaleProducts { get; set; }

    }
}
