using ConsoleTables;
using MarketConsole.Data.Models;
using MarketConsole.Services.Abstract;
using MarketManagement.HelpMenu;
using MarketSystems.Data.Common;
using MarketSystems.Data.Enums;
using MarketSystems.Data.Models;
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
                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Quantity");

                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Category, product.Quantity);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                int productQuantity = int.Parse(Console.ReadLine());

                var newID = marketable.AddProduct(productName, productPrice, productCategory, productQuantity);

                Console.WriteLine($"Product with ID {newID} was created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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

                Console.WriteLine("Please add new quantity of product:");
                int productQuantity = int.Parse(Console.ReadLine());

                marketable.UpdateProduct(productID, productName, productPrice, productCategory, productQuantity);

                Console.WriteLine($"Product with ID {productID} updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                var table = new ConsoleTable("ID", "Name", "Price", "Quantity");

                foreach (var product in productsInCategory)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Quantity);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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

                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Quantity");
                foreach (var product in productsInRange)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Category, product.Quantity);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                var table = new ConsoleTable("ID", "Name", "Price", "Category", "Quantity");
                foreach (var product in foundProducts)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Category, product.Quantity);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }
        public static void RemoveProductFromSale()
        {
            try
            {
                var sales = marketable.GetSale();

                Console.WriteLine("Enter the sale ID:");
                int saleId = int.Parse(Console.ReadLine());

                var sale = sales.FirstOrDefault(s => s.Id == saleId);

                if (sale == null)
                {
                    Console.WriteLine("Sale not found with the specified ID.");
                    return;
                }

                Console.WriteLine("Enter the product ID to remove from the sale:");
                int productId = int.Parse(Console.ReadLine());

                var saleItem = sale.SaleItems.FirstOrDefault(si => si.Product.Id == productId);

                if (saleItem == null)
                {
                    Console.WriteLine("Product not found in the sale.");
                    return;
                }

                Console.WriteLine("Enter the quantity to remove:");
                int quantityToRemove = int.Parse(Console.ReadLine());

                if (quantityToRemove <= 0)
                {
                    Console.WriteLine("Quantity to remove must be greater than zero.");
                    return;
                }

                if (quantityToRemove > saleItem.Quantity)
                {
                    Console.WriteLine("Quantity to remove is greater than the available quantity in the sale!");
                    return;
                }

                saleItem.Quantity -= quantityToRemove;

                if (saleItem.Quantity == 0)
                {
                    sale.SaleItems.Remove(saleItem);
                }

                if (sale.SaleItems.Count == 0)
                {
                    sales.Remove(sale);
                }

                Console.WriteLine("Product removed from the sale successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
            }
        }

        public static void ShowSalesByDate()
        {
            try
            {
                Console.WriteLine("Enter minDate for search (MM/dd/yyyy):");
                DateTime minDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter maxDate for search (MM/dd/yyyy):");
                DateTime maxDate = DateTime.Parse(Console.ReadLine());


                var foundSale = marketable.ShowSalesByDate(minDate, maxDate);

                if (foundSale.Count == 0)
                {
                    Console.WriteLine("Not found!");
                }
                var table = new ConsoleTable("ID", "Price", "Date", "Category");

                foreach (var sale in foundSale)
                {
                    foreach (var item in sale.SaleItems)
                    {
                        table.AddRow(sale.Id, sale.Amount, sale.Date, item.Product.Category);
                        break;
                    }

                }
                table.Write();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error! {ex.Message}");
            }
        }

        public static void ReturnPurchase()
        {
            try
            {
                var sales = marketable.GetSale();


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



        public static void ShowSalesOnExactDate()
        {
            try
            {
                Console.WriteLine("Enter the exact date to see sales on the given date (MM/dd/yyyy):");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());

                var salesOnExactDate = marketable.ShowSalesOnExactDate(dateTime);

                if (salesOnExactDate.Count == 0)
                {
                    Console.WriteLine("No sales found on the date you have entered!");
                    return;
                }

                var table = new ConsoleTable("ID", "Price", "Date", "Category");

                foreach (var sale in salesOnExactDate)
                {
                    foreach (var item in sale.SaleItems)
                    {
                        table.AddRow(sale.Id, sale.Amount, sale.Date, item.Product.Category);
                        break;
                    }
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops an error occurred: {ex.Message}");
            }
        }


        public static void ShowSalesByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter the minimum price for the sale:");
                decimal minPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter the maximum price for the sale:");
                decimal maxPrice = decimal.Parse(Console.ReadLine());

                var salePriceRange = marketable.ShowSaleByPriceRange(minPrice, maxPrice);
                if (salePriceRange.Count == 0)
                {
                    Console.WriteLine("No sales found within the price range!");
                    return;
                }

                var table = new ConsoleTable("ID", "Price", "Date", "Category");

                foreach (var sale in salePriceRange)
                {
                    foreach (var item in sale.SaleItems)
                    {
                        table.AddRow(sale.Id, sale.Amount, sale.Date, item.Product.Category);
                    }
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
            }
        }


        public static void ShowSalesByID()
        {
            try
            {
                Console.WriteLine("Enter the sale ID you want to see the information about:");
                int saleID = Convert.ToInt32(Console.ReadLine());

                var salesWithID = marketable.ShowSalesByID(saleID);
                if (salesWithID.Count == 0)
                {
                    Console.WriteLine("No sales found with the ID you have entered!");
                    return;
                }

                var table = new ConsoleTable("ID", "Product Name", "Price", "Date", "Quantity", "ItemID", "Category");

                foreach (var sale in salesWithID)
                {
                    foreach (var item in sale.SaleItems)
                    {
                        table.AddRow(sale.Id, item.Product.Name, sale.Amount, sale.Date, item.Quantity, item.Product.Id, item.Product.Category);
                    }
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
            }
        }

    }
}

