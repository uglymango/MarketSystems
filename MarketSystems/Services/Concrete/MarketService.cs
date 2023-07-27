using MarketConsole.Data.Models;
using MarketConsole.Services.Abstract;
using MarketSystems.Data.Enums;
using MarketSystems.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SalesItem = MarketConsole.Data.Models.SalesItem;

namespace MarketConsole.Services.Concrete
{
    public class MarketService : IMarkettable
    {
        private List<Product> products;
        private List<Sale> sales;
        private List<SalesItem> saleItems;

        public List<Product> GetProducts()
        {
            return products;
        }

        public List<Sale> GetSale()
        {
            return sales;
        }

        public MarketService()
        {
            products = new List<Product>();
            sales = new List<Sale>();
            saleItems = new List<SalesItem>();
        }

        public int AddProduct(string name, decimal price, ProductCategory category, int counts)
        {
            // Validation checks for the input parameters
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name is null!");

            if (price < 0)
                throw new ArgumentOutOfRangeException("Price is negative!");

            if (counts < 0)
                throw new ArgumentOutOfRangeException("Count can't be less than 0!");

            // Create a new product and add it to the list
            var product = new Product(name, price, category, counts);
            products.Add(product);
            return product.Id;
        }

        public void DeleteProduct(int ID)
        {
            if (ID < 0)
                throw new ArgumentOutOfRangeException("ID can't be negative!");

            var existingProduct = products.FirstOrDefault(p => p.Id == ID);
            if (existingProduct == null)
                throw new ArgumentNullException("Product not found!");

            products = products.Where(p => p.Id != ID).ToList();
        }

        public void UpdateProduct(int ID, string name, decimal price, ProductCategory category, int counts)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name is null!");

            if (price < 0)
                throw new ArgumentOutOfRangeException("Price is negative!");

            if (counts < 0)
                throw new ArgumentOutOfRangeException("Counts can't be negative!");

            var existingProduct = products.FirstOrDefault(p => p.Id == ID);
            if (existingProduct == null)
                throw new Exception("Product not found!");

            existingProduct.Name = name;
            existingProduct.Price = price;
            existingProduct.Category = category;
            existingProduct.Counts = counts;
        }

           public void ShowCategoryByProduct()
        {
            try
            {
                Console.Write("Choose the product category (0-7): ");
                int categoryNumber;
                while (!int.TryParse(Console.ReadLine(), out categoryNumber) ||
                       !Enum.IsDefined(typeof(ProductCategory), categoryNumber))
                {
                    Console.WriteLine("Invalid category selection. Please try again.");
                }

                ProductCategory category = (ProductCategory)categoryNumber;

                var productsInCategory = marketService.ShowCategoryByProduct(category);

                if (productsInCategory.Count == 0)
                {
                    Console.WriteLine($"No products found in the category: {category}");
                }
                else
                {
                    Console.WriteLine($"Products in the category: {category}");
                }
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while showing products by category: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }public List<Product> ShowCategoryByProduct(ProductCategory category)
        {
            if (category == null)
                throw new ArgumentNullException("Category can't be null!");

            return products.Where(x => x.Category == category).ToList();
        }

        public List<Product> FindProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name can't be empty!");

            return products.Where(x => x.Name == name).ToList();
        }

        public List<Product> ShowProductByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0)
                throw new Exception("Minimum price can't be less than 0");

            if (minPrice > maxPrice)
                throw new Exception("Minimum price can't be more than maximum price!");

            return products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
        }

        public void ShowSales()
        {
        }

        public void AddNewSale(int id, int count, DateTime dateTime)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be positive!");

            var product = products.Find(x => x.Id == id);

            if (product != null)
            {
                var price = product.Price * count;
                product.Counts -= count;

                var saleItem = new SalesItem(product, count);
                saleItems.Add(saleItem);

                var sale = new Sale(price, saleItem, dateTime);
                sales.Add(sale);
            }
            else
            {
                throw new Exception("Product with the given ID not found!");
            }
        }

        internal object FindProductById(int productID)
        {
            throw new NotImplementedException();
        }
    }
}

