using ConsoleTables;
using MarketConsole.Data.Models;
using MarketConsole.Services.Abstract;
using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Services.Concrete
{
    public class MenuService : BaseEntity
    {
        private static MarketService marketable = new MarketService();

        public static void ShowProducts()
        {
            try
            {
                var products = marketable.GetProducts();
                if (products.Count == 0)
                {
                    Console.WriteLine("There are no products!");
                    return;
                }

                Console.WriteLine("All Products:");
                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");

                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Category, product.Counts);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void AddNewProduct()
        {
            try
            {
                Console.WriteLine("Please add product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Please add price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Please add category of product:");
                ProductCategory productCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);

                Console.WriteLine("Please add count of product:");
                int productCount = int.Parse(Console.ReadLine());

                var newID = marketable.AddProduct(productName, productPrice, productCategory, productCount);

                Console.WriteLine($"Product with ID {newID} was created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Please enter product ID for the product to be updated:");
                int productID = int.Parse(Console.ReadLine());

                var productToUpdate = marketable.FindProductById(productID);

                Console.WriteLine("Please add new product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Please add new price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Please add new category of product:");
                ProductCategory productCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);

                Console.WriteLine("Please add new count of product:");
                int productCount = int.Parse(Console.ReadLine());

                marketable.UpdateProduct(productID, productName, productPrice, productCategory, productCount);

                Console.WriteLine($"Product with ID {productID} updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void RemoveProduct()
        {
            try
            {
                Console.WriteLine("Please enter product ID to remove the product:");
                int productID = int.Parse(Console.ReadLine());

                marketable.DeleteProduct(productID);

                Console.WriteLine($"Product with ID {productID} removed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void ShowCategoryByProduct()
        {
            try
            {
                Console.WriteLine("You can see all product categories below:");
                Console.WriteLine("Categories: Foods, Electronics, Clothing, Groceries, HomeAndKitchen, BeautyAndPersonalCare, Books, Others.");

                Console.WriteLine("\nSelect category to show products:");
                string categoryName = Console.ReadLine();

                if (!Enum.TryParse<ProductCategory>(categoryName, true, out var productCategory))
                {
                    Console.WriteLine("Invalid category name!");
                    return;
                }

                var productsInCategory = marketable.ShowCategoryByProduct(productCategory).ToList();

                if (productsInCategory.Count == 0)
                {
                    Console.WriteLine("No products found in this category!");
                    return;
                }

                Console.WriteLine($"Products in {productCategory} category:");
                var table = new ConsoleTable("ID", "Name", "Price", "Count");

                foreach (var product in productsInCategory)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Counts);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void ShowProductByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter minimum price for searching products:");
                decimal minPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter maximum price for searching products:");
                decimal maxPrice = decimal.Parse(Console.ReadLine());

                var productsInRange = marketable.ShowProductByPriceRange(minPrice, maxPrice).ToList();

                if (productsInRange.Count == 0)
                {
                    Console.WriteLine("No products found within this price range!");
                    return;
                }

                Console.WriteLine($"Products between {minPrice} and {maxPrice}:");

                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");
                foreach (var product in productsInRange)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Category, product.Counts);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void FindProductByName()
        {
            try
            {
                Console.WriteLine("Please enter product name to find:");
                string name = Console.ReadLine();

                var foundProducts = marketable.FindProductByName(name).ToList();

                if (foundProducts.Count == 0)
                {
                    Console.WriteLine("Product not found!");
                    return;
                }

                Console.WriteLine($"Products with name '{name}':");
                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");
                foreach (var product in foundProducts)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Category, product.Counts);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void ShowSales()
        {
            try
            {
                var sales = marketable.GetSale();
                if (sales.Count == 0)
                {
                    Console.WriteLine("There are no sales!");
                    return;
                }

                var table = new ConsoleTable("ID", "Amount", "Date");

                foreach (var sale in sales)
                {
                    table.AddRow(sale.Id, sale.Amount, sale.Date.ToString("dd/MM/yyyy"));
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void AddNewSales()
        {
            try
            {
                Console.WriteLine("Please add product ID for sales:");
                int salesID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the counts:");
                int counts = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter date (dd/MM/yyyy):");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());

                marketable.AddNewSale(salesID, counts, dateTime);

                Console.WriteLine("Sale added successfully!");
                Console.WriteLine("------------------------");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter valid data.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding sale: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }
    }
}

