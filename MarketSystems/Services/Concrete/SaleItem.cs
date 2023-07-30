namespace MarketConsole.Data.Models
{
    public class SaleItem
    {
        public SaleItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

