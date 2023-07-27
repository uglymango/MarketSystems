using MarketConsole.Data.Models;
using MarketSystems.Data.Enums;
using MarketSystems.Data.Models;
using System;
using System.Collections.Generic;

namespace MarketConsole.Services.Abstract
{
    public interface IMarkettable
    {
        List<Product> GetProducts();
        List<Sale> GetSale();
        int AddProduct(string name, decimal price, ProductCategory category, int counts);
        void DeleteProduct(int ID);
        void UpdateProduct(int ID, string name, decimal price, ProductCategory category, int counts);
        List<Product> ShowCategoryByProduct(ProductCategory category);
        List<Product> FindProductByName(string name);
        List<Product> ShowProductByPriceRange(decimal minPrice, decimal maxPrice);
        void AddNewSale(int id, int count, DateTime dateTime);
    }
}
