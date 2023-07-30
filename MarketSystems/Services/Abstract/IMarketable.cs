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
        void DeleteProduct(int ID);
        void UpdateProduct(int ID, string name, decimal price, ProductCategory category, int quantity);
        List<Product> ShowCategoryByProduct(ProductCategory category);
        List<Product> FindProductByName(string name);
        List<Product> ShowProductByPriceRange(decimal minPrice, decimal maxPrice);
        void AddNewSale(int id, int count, DateTime dateTime);
        void RemoveProductFromSale();
        void ReturnPurchase();
        List<Sale> ShowSalesByDate(DateTime minDate, DateTime maxDate);
        List<Sale> ShowSalesByPriceRange(decimal minAmount, decimal maxAmount);
        List<Sale> ShowSalesOnExactDate(DateTime dateTime);
        List<Sale> ShowSalesByID(int Id);
        List<ProductCategory> GetProductCategories();
        int AddProductWithCategory(string name, decimal price, string category, int quantity);
    }
}
