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

               //Here we use linq's find method to find the product
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
                    //Here this returns sale not found if id is invaild
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

            //We find all sales within the date range we have entered before
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
            //We find all sales within the price range we have entered before
            List<Sale> foundSales = sales.FindAll(sale => sale.Amount >= minAmount && sale.Amount <= maxAmount);

            return foundSales;
        }

        //Added this method so we could get a list of categories when we wanna add a product
        public List<ProductCategory> GetProductCategories()
        {
            return Enum.GetValues(typeof(ProductCategory)).Cast<ProductCategory>().ToList();
        }

        //The method here is called so because it lets us add a new product to the market with the specific details including category
        public int AddProductWithCategory(string name, decimal price, string category, int quantity)
        {
           
            //We use TryParse here to convert string into enum
            if (!Enum.TryParse<ProductCategory>(category, true, out var productCategory))
            {
                Console.WriteLine("Invalid product category.");
                return -1;
            }

            //Here we check if the entered name is null or not 
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Product name cannot be null or whitespace!");
                return -1;
            }

           //And here we check if the price equals to 0 or its less than 0
            if (price <= 0)
            {
                Console.WriteLine("Price cannot be negative!");
                return -1;
            }

            //Here we don't check if quantity is 0 cuz we might have ran out of stock but we still have the product
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
            //Here wr check if the id is less than 0 or not
            if (ID < 0)
                throw new ArgumentOutOfRangeException("Product ID cannot be negative!");

            var existingProduct = products.FirstOrDefault(p => p.Id == ID);
            if (existingProduct == null)
                throw new ArgumentNullException("Could not find the product!");

            products = products.Where(p => p.Id != ID).ToList();
        }

        public void UpdateProduct(int ID, string name, decimal price, ProductCategory category, int quantity)
        {
            //Check if any of the entered parameters (name/price/quantity) is null
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name cannot be null!");

            if (price < 0)
                throw new ArgumentOutOfRangeException("Price cannot be negative!");

            if (quantity < 0)
                throw new ArgumentOutOfRangeException("Quantity cannot be negative!");

            var existingProduct = products.FirstOrDefault(p => p.Id == ID);
            if (existingProduct == null)
                throw new Exception("Could not find the product!");

            existingProduct.Name = name;
            existingProduct.Price = price;
            existingProduct.Category = category;
            existingProduct.Quantity = quantity;
        }

        public List<Product> ShowCategoryByProduct(ProductCategory category)
        {
            
            //Here we check if the category is null (even if we don't need cuz i started count from 1)
            if (category == null)
            {
                
                return products.ToList();
            }
            //And it filters and returns us the products in the category we asked for
            return products.Where(x => x.Category == category).ToList();
        }


        public List<Product> FindProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be empty!");
          
            //Here it filters all products and returns filtered ones which include the name we input
            return products.Where(x => x.Name == name).ToList();
        }

        public List<Product> ShowProductByPriceRange(decimal minPrice, decimal maxPrice)
        {
            //We check if minimum price is less than 0 or not
            if (minPrice < 0)
                throw new Exception("Minimum price cannot be less than 0");

            //And here we cjeck if minimum price is more than maximum price
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



