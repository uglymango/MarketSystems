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

        public void RemoveProductFromSale()
        {
            try
            {
                Console.WriteLine("Enter the sale ID to remove the product:");
                int saleId = int.Parse(Console.ReadLine());

                var sale = sales.Find(s => s.Id == saleId);

                if (sale != null)
                {
                    foreach (var saleItem in sale.SaleItems)
                    {
                        var product = saleItem.Product;
                        product.Quantity += saleItem.Quantity;
                    }

                    sales.Remove(sale);
                    Console.WriteLine("Product removed from the sale successfully!");
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

        public void ReturnPurchase()
        {
            try
            {
                Console.WriteLine("Enter the sale ID to return the purchase:");
                int saleId = int.Parse(Console.ReadLine());

                var sale = sales.FirstOrDefault(s => s.Id == saleId);

                if (sale != null)
                {
                    if (sale.SaleItems != null)
                    {
                        foreach (var saleItem in sale.SaleItems)
                        {
                            var product = saleItem.Product;
                            product.Quantity += saleItem.Quantity;
                        }
                    }

                    sales.Remove(sale);
                    Console.WriteLine("Purchase returned successfully!");
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
                Console.WriteLine($"Error while returning purchase: {ex.Message}");
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

        public List<Sale> ShowSalesOnExactDate(DateTime dateTime)
        {
            List<Sale> salesOnExactDate = sales.FindAll(sale => sale.Date.Date == dateTime.Date);

            return salesOnExactDate;
        }

        public List<Sale> ShowSalesByPriceRange(decimal minAmount, decimal maxAmount)
        {
            List<Sale> foundSales = sales.FindAll(sale => sale.Amount >= minAmount && sale.Amount <= maxAmount);

            return foundSales;
        }

        public List<ProductCategory> GetProductCategories()
{
    return Enum.GetValues(typeof(ProductCategory)).Cast<ProductCategory>().ToList();
}

        public int AddProductWithCategory(string name, decimal price, string category, int quantity)
        {
            if (!Enum.TryParse<ProductCategory>(category, true, out var productCategory))
            {
                Console.WriteLine("Invalid product category.");
                return -1;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Product name cannot be null or whitespace!");
                return -1;
            }

            if (price < 0)
            {
                Console.WriteLine("Price cannot be negative!");
                return -1;
            }

            if (quantity < 0)
            {
                Console.WriteLine("Product count cannot be less than 0.");
                return -1;
            }

            var product = new Product(name, price, productCategory, quantity);

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

        public void AddNewSale(int id, int count, DateTime dateTime)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Quantity cannot be negative!");

            var product = products.Find(x => x.Id == id);

            if (product != null && product.Quantity >= count)
            {
                var price = product.Price * count;
                product.Quantity -= count;

                var saleItem = new SaleItem(product, count);

                var saleItemsList = new List<SaleItem> { saleItem };

                var sale = new Sale(price, saleItemsList, dateTime);
                sales.Add(sale);
            }
            else
            {
                throw new Exception("Product with the given ID not found or insufficient quantity!");
            }
        }

        public List<Sale> ShowSalesByID(int Id)
        {
            List<Sale> salesWithMatchingID = sales.FindAll(sale => sale.Id == Id);

            return salesWithMatchingID;
        }
    }
}



