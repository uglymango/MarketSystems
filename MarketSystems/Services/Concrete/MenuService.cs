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

        public static void AddProduct()
        {
            try
            {
                Console.WriteLine("Please add product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Please add price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Please select category of product:");
                var categories = marketable.GetProductCategories();
                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {categories[i]}");
                }

                int categoryChoice = int.Parse(Console.ReadLine());
                if (categoryChoice < 1 || categoryChoice > categories.Count)
                {
                    Console.WriteLine("Invalid category choice.");
                    return;
                }

                ProductCategory productCategory = categories[categoryChoice - 1];

                Console.WriteLine("Please add count of product:");
                int productQuantity = int.Parse(Console.ReadLine());

                var newID = marketable.AddProductWithCategory(productName, productPrice, productCategory.ToString(), productQuantity);

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
                Console.WriteLine("Enter the ID of the product you want to change:");
                int numberID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new name of the product:");
                string productName = Console.ReadLine();

                Console.WriteLine("Enter the new price of the product:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new category of the product:");
                ProductCategory productCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);

                Console.WriteLine("Enter the new quantity of the product:");
                int productQuantity = int.Parse(Console.ReadLine());

                marketable.UpdateProduct(numberID, productName, productPrice, productCategory, productQuantity);

                Console.WriteLine($"Product has been updated successfully!");

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

                var table = new ConsoleTable("ID", "Amount", "Quantity", "Date");

                foreach (var sale in sales)
                {
                    var totalQuantity = sale.SaleItems.Sum(si => si.Quantity);
                    table.AddRow(sale.Id, sale.Amount, totalQuantity, sale.Date.ToString("dd/MM/yyyy"));
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
            }
        }

        public static void AddNewSale()
        {
            try
            {
                Console.WriteLine("Please enter the date of the sale (MM/dd/yyyy):");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());

                var products = marketable.GetProducts();
                Console.WriteLine("Available Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Category: {product.Category}, Quantity: {product.Quantity}");
                }

                var saleItems = new List<SaleItem>();

                //The while loop allows us to add more than 1 product to the sale.
                
                while (true)
                {
                    Console.WriteLine("Enter the ID of the product to add to the sale (or enter 'done' to finish adding products):");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "done")
                        break;

                    if (int.TryParse(input, out int productId))
                    {
                        var product = products.FirstOrDefault(p => p.Id == productId);
                        if (product != null)
                        {
                            Console.WriteLine("Enter the quantity of the product:");
                            int quantity = int.Parse(Console.ReadLine());

                            if (quantity <= 0)
                            {
                                Console.WriteLine("Quantity must be greater than 0. Please try again.");
                                continue;
                            }

                            if (quantity > product.Quantity)
                            {
                                Console.WriteLine("Quantity is more than the available stock. Please try again.");
                                continue;
                            }

                            saleItems.Add(new SaleItem(product, quantity));
                            product.Quantity -= quantity;
                        }
                        else
                        {
                            Console.WriteLine($"Product with ID {productId} not found. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid product ID or 'done' to finish adding products.");
                    }
                }

                if (saleItems.Count == 0)
                {
                    Console.WriteLine("No products added to the sale. Sale creation cancelled.");
                    return;
                }

                decimal totalPrice = saleItems.Sum(si => si.Product.Price * si.Quantity);
                var newSale = new Sale(totalPrice, saleItems, dateTime);
                marketable.GetSale().Add(newSale);

                Console.WriteLine("Sale created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                Console.WriteLine("Enter minimum date for the search (dd/MM/yyyy):");
                DateTime minDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter maximum date for the search (dd/MM/yyyy):");
                DateTime maxDate = DateTime.Parse(Console.ReadLine());

                var foundSale = marketable.ShowSalesByDate(minDate, maxDate);

                if (foundSale.Count == 0)
                {
                    Console.WriteLine("Could not find a sale!");
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
                Console.WriteLine($"Oops, an error occurred: {ex.Message}");
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
                Console.WriteLine("Enter the exact date to see sales on the given date (dd/MM/yyyy):");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());

                List<Sale> salesOnExactDate = marketable.ShowSalesOnExactDate(dateTime);

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
                        table.AddRow(sale.Id, sale.Amount, sale.Date.ToString("yyyy-MM-dd"), item.Product.Category);
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
                decimal minAmount = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter the maximum price for the sale:");
                decimal maxAmount = decimal.Parse(Console.ReadLine());

                var salePriceRange = marketable.ShowSalesByPriceRange(minAmount, maxAmount);
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

