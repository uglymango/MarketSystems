using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using MarketConsole.Data.Models;
using MarketConsole.Services.Concrete;
using MarketSystems.Data.Models;
using SaleItem = MarketSystems.Data.Models.SaleItem;

namespace MarketManagement.HelpMenu
{
    public class ManageSales
    {
        private List<Product> products;
        private List<Sale> sales;

        public ManageSales()
        {
            products = new List<Product>();
            sales = new List<Sale>();
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
                    List<string> productNames = new List<string>();
                    foreach (var saleItem in sale.SaleItems)
                    {
                        productNames.Add(saleItem.Product.Name);
                    }
                    string productNamesStr = string.Join(", ", productNames);

                    table.AddRow(sale.Id, sale.Date.ToString("yyyy-MM-dd"), productNamesStr, sale.Amount);
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


        public void AddNewSale(int productId, int quantity, DateTime dateTime)
        {
            try
            {
                if (products == null)
                {
                    Console.WriteLine("Products list is null. Cannot add sale.");
                    Console.WriteLine("------------------------");
                    return;
                }

                var product = products.Find(p => p.Id == productId);

                if (quantity <= 0)
                    throw new Exception("Quantity should be greater than zero!");

                if (product != null && product.Quantity >= quantity)
                {
                    decimal totalPrice = product.Price * quantity;

                    var saleItems = new List<SaleItem>();
                    var saleItem = new SaleItem(product, quantity);
                    saleItems.Add(saleItem);

                    int option;
                    do
                    {
                        Console.WriteLine("Select one of the options to continue adding sales or finish!");
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");

                        while (!int.TryParse(Console.ReadLine(), out option))
                        {
                            Console.WriteLine("Invalid option!");
                            Console.WriteLine("Enter option again:");
                        }

                        switch (option)
                        {
                            case 1:
                                Console.WriteLine("Enter product ID of the new sale item:");
                                int salesID = int.Parse(Console.ReadLine());

                                Console.WriteLine("Enter quantity of the new sale item:");
                                int countSale = int.Parse(Console.ReadLine());

                                var newProduct = products.Find(p => p.Id == salesID);

                                if (newProduct != null && newProduct.Quantity >= countSale)
                                {
                                    decimal secondPrice = newProduct.Price * countSale;
                                    newProduct.Quantity -= countSale;
                                    var newSaleItem = new SaleItem(newProduct, countSale);
                                    saleItems.Add(newSaleItem);
                                    totalPrice += secondPrice;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid product or insufficient stock!!");
                                }
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine("No such option!");
                                break;
                        }
                    } while (option != 2);

                    var sale = new Sale(totalPrice, dateTime);
                    foreach (var item in saleItems)
                    {
                        sale.AddSaleItem(item);
                    }

                    sales.Add(sale); 

                    Console.WriteLine("Sale added successfully!");
                    Console.WriteLine("------------------------");
                }
                else
                {
                    Console.WriteLine("Invalid product or insufficient stock for the sale.");
                    Console.WriteLine("------------------------");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter valid values.");
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding sale: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public void ReturnPurchase(int saleId)
        {
            try
            {
                var sale = sales.Find(s => s.Id == saleId);
                if (sale != null)
                {
                    decimal totalPriceToReturn = 0;

                    foreach (var saleItem in sale.SaleItems)
                    {
                        var product = saleItem.Product;
                        product.Counts += saleItem.Quantity; 
                        totalPriceToReturn += saleItem.Quantity * product.Price; 
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



        public void RemoveSale(int saleId)
        {
            try
            {
                var sale = sales.Find(s => s.Id == saleId);
                if (sale != null)
                {
                    foreach (var saleItem in sale.SaleItems)
                    {
                        var product = saleItem.product;
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
                Console.WriteLine($"Error while removing sale: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public void ShowSalesByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                Console.WriteLine($"Sales between {startDate.ToString("yyyy-MM-dd")} and {endDate.ToString("yyyy-MM-dd")}:");
                var table = new ConsoleTable("ID", "Date", "Product", "Total Price");

                foreach (var sale in sales)
                {
                    if (sale.Date >= startDate && sale.Date <= endDate)
                    {
                        StringBuilder productNames = new StringBuilder();

                        foreach (var saleItem in sale.SaleItems)
                        {
                            productNames.Append(saleItem.Product.Name).Append(", ");
                        }

                        if (productNames.Length > 0)
                        {
                            productNames.Length -= 2; 
                        }

                        table.AddRow(sale.Id, sale.Date.ToString("yyyy-MM-dd"), productNames.ToString(), sale.Amount);
                    }
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

        public void ShowSalesByAmountRange(decimal minAmount, decimal maxAmount)
        {
            try
            {
                Console.WriteLine($"Sales within the amount range: {minAmount} to {maxAmount}");
                var table = new ConsoleTable("ID", "Date", "Product", "Total Price");

                foreach (var sale in sales)
                {
                    if (sale.Amount >= minAmount && sale.Amount <= maxAmount)
                    {
                        List<string> productNames = new List<string>();
                        foreach (var saleItem in sale.SaleItems)
                        {
                            productNames.Add(saleItem.Product.Name);
                        }

                        table.AddRow(sale.Id, sale.Date.ToString("yyyy-MM-dd"), string.Join(", ", productNames), sale.Amount);
                    }
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

        public void SearchBySaleDate(DateTime date)
        {
            try
            {
                Console.WriteLine($"Sales on the specified date: {date.Date.ToString("yyyy-MM-dd")}");
                var table = new ConsoleTable("ID", "Date", "Product", "Total Price");

                foreach (var sale in sales)
                {
                    if (sale.Date.Date == date.Date)
                    {
                        string productNames = string.Join(", ", sale.SaleItems.Select(saleItem => saleItem.Product.Name));
                        table.AddRow(sale.Id, sale.Date.ToString("yyyy-MM-dd"), productNames, sale.Amount);
                    }
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
        public void ShowSaleByID(int saleId)
        {
            try
            {
                var sale = sales.Find(s => s.Id == saleId);
                if (sale != null)
                {
                    Console.WriteLine("Sale Details:");
                    var table = new ConsoleTable("ID", "Date", "Product", "Total Price");

                    foreach (var saleItem in sale.SaleItems)
                    {
                        if (saleItem.Product is Product product) 
                        {
                            string productName = product.Name;
                            table.AddRow(sale.Id, sale.Date.ToString("yyyy-MM-dd"), productName, saleItem.Quantity * product.Price);
                        }
                    }

                    table.Write();
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
    }
}

