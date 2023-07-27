namespace MarketConsole.Data.Models
{
    public class SalesItem
    {
        public SalesItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

