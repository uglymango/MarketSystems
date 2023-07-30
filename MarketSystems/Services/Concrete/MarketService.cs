using ConsoleTables;
using MarketConsole.Data.Models;
using MarketConsole.Services.Abstract;
using MarketSystems.Data.Enums;
using MarketSystems.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SaleItem = MarketConsole.Data.Models.SaleItem;

namespace MarketConsole.Services.Concrete
{
    public class MarketService : IMarkettable
    {
        private List<Product> products;
        private List<Sale> sales;
        private List<SaleItem> saleItems;

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
            saleItems = new List<SaleItem>();
        }

        public int AddProduct(string name, decimal price, ProductCategory category, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be null or whitespace!");

            if (price < 0)
                throw new ArgumentException("Price cannot be negative!");

            if (quantity < 0)
                throw new ArgumentException("Product count cannot be less than 0.");

            var product = new Product(name, price, category, quantity);

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

        public void UpdateProduct(int ID, string name, decimal price, ProductCategory category, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name cannot be null!");

            if (price < 0)
                throw new ArgumentOutOfRangeException("Price cannot be negative!");

            if (quantity < 0)
                throw new ArgumentOutOfRangeException("Quantity cannot be negative!");

            var existingProduct = products.FirstOrDefault(p => p.Id == ID);
            if (existingProduct == null)
                throw new Exception("Product not found!");

            existingProduct.Name = name;
            existingProduct.Price = price;
            existingProduct.Category = category;
            existingProduct.Quantity = quantity;
        }

        public void ShowCategoryByProduct()
        {
            try
            {
                var productsByCategory = products.GroupBy(p => p.Category);

                foreach (var categoryGroup in productsByCategory)
                {
                    var category = categoryGroup.Key;
                    var productsInCategory = categoryGroup.ToList();

                    Console.WriteLine($"Category: {category}");

                    foreach (var product in productsInCategory)
                    {
                        Console.WriteLine($" Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                    }

                    Console.WriteLine("------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }
    public List<Product> ShowCategoryByProduct(ProductCategory category)
        {
            if (category == null)
                throw new ArgumentNullException("Category cannot be null!");

            return products.Where(x => x.Category == category).ToList();
        }

        public List<Product> FindProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be empty!");

            return products.Where(x => x.Name == name).ToList();
        }

        public List<Product> ShowProductByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0)
                throw new Exception("Minimum price cannot be less than 0");

            if (minPrice > maxPrice)
                throw new Exception("Minimum price cannot be more than maximum price!");

            return products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
        }

        public void ShowSales()
        {
            try
            {
                if (sales.Count == 0)
                {
                    Console.WriteLine("There are no sales!");
                    return;
                }

                Console.WriteLine("All Sales:");
                var table = new ConsoleTable("ID", "Date", "Product", "Total Price");

                foreach (var sale in sales)
                {
                    string productNames = string.Join(", ", sale.SaleItems.Select(saleItem => saleItem.Product.Name));
                    table.AddRow(sale.Id, sale.Date.ToString("yyyy-MM-dd"), productNames, sale.Amount);
                }

                table.Write();
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public void AddNewSale(int id, int quantity, DateTime dateTime)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Quantity cannot be negative!");

            var product = products.Find(x => x.Id == id);

            if (product != null && product.Quantity >= quantity)
            {
                var price = product.Price * quantity;
                product.Quantity -= quantity;

                var saleItem = new SaleItem(product, quantity);

                var saleItemsList = new List<SaleItem> { saleItem };

                var sale = new Sale(price, saleItemsList, dateTime);
                sales.Add(sale);
            }
            else
            {
                throw new Exception("Product with the given ID not found or insufficient quantity!");
            }
        }

        public void RemoveSale(int saleId)
        {
            try
            {
                var sale = sales.Find(s => s.Id == saleId);
                if (sale != null)
                {
                    foreach (var saleItem in sale.SaleItems)
                    {
                        var product = saleItem.Product;
                        product.Quantity += saleItem.Quantity;
                    }

                    sales.Remove(sale);
                    Console.WriteLine("Sale removed successfully!");
                    Console.WriteLine("------------------------");
                }
                else
                {
                    Console.WriteLine("Sale not found with the specified ID.");
                    Console.WriteLine("------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public List<Sale> ShowSalesByDate(DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate)
                throw new ArgumentException("Minimum date cannot be greater than Maximum date!");

            List<Sale> foundSales = sales.FindAll(sale => sale.Date >= minDate && sale.Date <= maxDate);

            return foundSales;
        }

        public List<Sale> ShowSaleByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
                throw new ArgumentException("Invalid price range!");

            List<Sale> salePriceRange = sales.FindAll(sale => sale.Amount >= minPrice && sale.Amount <= maxPrice);

            return salePriceRange;
        }

        public List<Sale> ShowSalesByID(int saleID)
        {
            List<Sale> salesWithMatchingID = sales.FindAll(sale => sale.Id == saleID);

            return salesWithMatchingID;
        }

        public List<Sale> ShowSalesOnExactDate(DateTime dateTime)
        {
            List<Sale> salesOnExactDate = sales.FindAll(sale => sale.Date.Date == dateTime.Date);

            return salesOnExactDate;
        }

        internal object FindProductById(int productID)
        {
            throw new NotImplementedException();
        }
    }
}

