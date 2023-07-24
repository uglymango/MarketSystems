using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystems.Data.Models
{
    public class Product : BaseEntity
    {
        private static int count = 0;

        public Product(string name, decimal price, Categories category, int amount)
        {
            Name = name;
            Price = price;
            Category = category;
            Amount = amount;
            
            Id = count;
            count++;
        }


        public string Name { get; set; }
        public decimal Price { get; set; }
        public Categories Category { get; set; }
        public int Amount { get; set; }

    }
}
