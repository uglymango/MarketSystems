using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Data.Models
{
    public class Product : BaseEntity
    {
        private static int count = 0;
        public Product(string name, decimal price, ProductCategory category, int counts)
        {
            Name = name;
            Price = price;
            Category = category;
            Counts = counts;

            Id = count;
            count++;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public int Counts { get; set; }
        public Product(string name, decimal price, ProductCategory category, int counts, int id)
        {
            Name = name;
            Price = price;
            Category = category;
            Counts = counts;
            Id = id;

        }
    }

}
