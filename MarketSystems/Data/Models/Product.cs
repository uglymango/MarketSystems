using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;

namespace MarketConsole.Data.Models
{
    public class Product : BaseEntity
    {
        private static int count = 0;

        public Product(string name, decimal price, ProductCategory category, int quantity)
        {
            Name = name;
            Price = price;
            Category = category;
            Quantity = quantity;
            Id = count;
            count++;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public int Quantity { get; set; } 

        public Product(string name, decimal price, ProductCategory category, int quantity, int id)
        {
            Name = name;
            Price = price;
            Category = category;
            Quantity = quantity;
            Id = id;
        }
    }
}
