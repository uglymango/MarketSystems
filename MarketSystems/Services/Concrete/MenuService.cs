using ConsoleTables;
using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsole.Services.Concrete.MenuServices
{
    public class MenuService : BaseEntity
    {
        private static Marketable marketable = new Marketable();
        private static object table;

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

                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Count");

                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Category, product.Quantity);
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
                Console.WriteLine("Please add products name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Please add price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Please add category of product:");
                ProductCategory productCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);

                Console.WriteLine("Please add quantity of product:");
                int productQuantity = int.Parse(Console.ReadLine());

                var newID = marketable.AddProduct(productName, productPrice, productCategory, productQuantity);

                Console.WriteLine($"Product with ID {newID} was created!");


            }

            catch (Exception ex)
            {
                Console.WriteLine();
            }

        }

        public static void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Please add ID for change product:");
                int numberID = int.Parse(Console.ReadLine());

                Console.WriteLine("Please add  new product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Please add new price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Please add new category of product:");
                ProductCategory productCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);

                Console.WriteLine("Please add new count of product:");
                int productCount = int.Parse(Console.ReadLine());

                marketable.UpdateProduct(numberID, productName, productPrice, productCategory, productCount);

                Console.WriteLine($"Product uptade is succesfully!");


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
                Console.WriteLine("Please enter product ID for deleting product!");
                int newID = int.Parse(Console.ReadLine());

                marketable.DeleteProduct(newID);

                Console.WriteLine("Deleting produtc was succesfully!");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }
        public static void ShowCategoryByProduct()
        {
            Console.WriteLine("You can see all product categories in the below:");
            Console.WriteLine(ProductCategory.Foods);
            Console.WriteLine(ProductCategory.Electronics);
            Console.WriteLine(ProductCategory.Clothing);
            Console.WriteLine(ProductCategory.HomeAndKitchen);
            Console.WriteLine(ProductCategory.Groceries);
            Console.WriteLine(ProductCategory.Books);
            Console.WriteLine(ProductCategory.BeautyAndPersonalCare);
            Console.WriteLine(ProductCategory.HomeAndKitchen);
            Console.WriteLine(ProductCategory.Others);


        }
        public static void ShowProductByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter minimum price for searching products:");
                int minPrice = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter maximum price for searching products:");
                int maxPrice = int.Parse(Console.ReadLine());

                var priceRange = marketable.ShowProductByPriceRange(minPrice, maxPrice);
                if (priceRange.Count == 0)
                {
                    Console.WriteLine("No products found!");
                }

                foreach (var price in priceRange)
                {
                    Console.WriteLine($"ID: {price.Id} | Name: {price.Name} | Price: {price.Price} | Category: {price.Category} | Count: {price.Quantity}");
                }


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
                Console.WriteLine("Please enter product name for finding:");
                string name = Console.ReadLine();

                var searchName = marketable.FindProductByName(name);
                if (searchName.Count == 0)
                {
                    Console.WriteLine("Product not found!");
                    return;
                }

                foreach (var item in searchName)
                {
                    Console.WriteLine($"ID: {item.Id} | Name: {item.Name} | Price: {item.Price} | Category: {item.Category} | Count: {item.Quantity}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! {ex.Message}");

            }

        }
    }


}
