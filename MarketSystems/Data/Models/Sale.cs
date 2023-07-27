using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System;
using System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketSystems.Data.Models
{
    public class Sale : BaseEntity
    {
        private static int count = 0;

        public Sale(decimal totalAmount, List<SalesItem> saleItems, DateTime date)
        {
            Amount = totalAmount;
            SaleItems = saleItems;
            Date = date;

            Id = count;
            count++;
        }

        public Sale(decimal totalAmount, MarketConsole.Data.Models.SalesItem saleItem, DateTime dateTime)
        {
        }

        public decimal Amount { get; set; }
        public List<SalesItem> SaleItems { get; set; }
        public DateTime Date { get; set; }
    }
}

