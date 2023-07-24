using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketSystems.Data.Models
{
    public class Sale : BaseEntity
    {

        private static int count = 0;

        public Sale(decimal amount, string saleproducts, DateTime date)
        {
            Amount = amount;
            SaleProducts = saleproducts;
            Date = date;
            
            Id = count;
            count++;
        }
        public decimal Amount { get; set; }
        public string SaleProducts { get; set; }
        public DateTime Date { get; set; }

    }
}
