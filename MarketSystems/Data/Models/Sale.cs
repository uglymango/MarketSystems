using MarketConsole.Data.Models;
using MarketSystems.Data.Common;

namespace MarketSystems.Data.Models
{
    public class Sale : BaseEntity
    {
        private static int count = 0;


        public Sale(decimal price, List<SaleItem> saleItems, DateTime dateTime)
        {

            Amount = price;
            SaleItems = saleItems;
            Date = dateTime;
            Id = count;
            count++;
        }

        public decimal Amount { get; set; }
        public List<SaleItem> SaleItems { get; set; }
        public DateTime Date { get; set; }

        internal void AddSaleItem(SaleItem item)
        {
            SaleItems.Add(item);
        }
    }
}