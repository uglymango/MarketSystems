using MarketSystems.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketManagement.HelpMenu
{
    public class SubMenu
    {
        public static void ManageProducts()
        {
            ManageProducts manageProducts = new ManageProducts();

            Console.Clear();
            int option;

            do
            {
                Console.WriteLine("1. Show products.");
                Console.WriteLine("2. Add new product.");
                Console.WriteLine("3. Update product.");
                Console.WriteLine("4. Remove product.");
                Console.WriteLine("5. Show category by product.");
                Console.WriteLine("6. Show product by price range.");
                Console.WriteLine("7. Find product by name.");
                Console.WriteLine("0. Back to the main menu.");
                Console.WriteLine("------------------------");
                Console.WriteLine("Please, enter a valid option:");
                Console.WriteLine("------------------------");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Please, enter a valid option:");
                    Console.WriteLine("------------------------");
                }

                switch (option)
                {
                    case 1:
                        manageProducts.ShowProducts(); 
                        break;
                    case 2:
                        manageProducts.AddNewProduct();
                        break;
                    case 3:
                        manageProducts.UpdateProduct();
                        break;
                    case 4:
                        manageProducts.RemoveProduct();
                        break;
                    case 5:
                        manageProducts.ShowCategoryByProduct();
                        break;
                    case 6:
                        manageProducts.ShowProductByPriceRange();
                        break;
                    case 7:
                        manageProducts.FindProductByName();
                        break;
                    case 0:
                        Console.WriteLine("Going back to the main menu.");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

            } while (option != 0);
        }

        private static object GetManageProducts()
        {
            return ManageProducts;
        }

        public static void ManageSales()
        {
            int option;

            do
            {
                Console.WriteLine("1. Show sales.");
                Console.WriteLine("2. Return purchase.");
                Console.WriteLine("3. Remove sales.");
                Console.WriteLine("4. Add new sales.");
                Console.WriteLine("5. Show sales by date.");
                Console.WriteLine("6. Show sales by amount range.");
                Console.WriteLine("7. Search by sale date.");
                Console.WriteLine("8. Show sales by ID.");
                Console.WriteLine("0. Back to the main menu.");
                Console.WriteLine("------------------------");
                Console.WriteLine("Please, enter a valid option:");
                Console.WriteLine("------------------------");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Please, enter a valid option:");
                    Console.WriteLine("------------------------");
                }

                switch (option)
                {
                    case 1:
                        ShowSales(); 
                        break;
                    case 2:
                        ReturnPurchase(); 
                        break;
                    case 3:
                        RemoveSales(); 
                        break;
                    case 4:
                        AddNewSales(); 
                        break;
                    case 5:
                        ShowSalesByDate(); 
                        break;
                    case 6:
                        ShowSalesByAmountRange(); 
                        break;
                    case 7:
                        SearchBySaleDate(); 
                        break;
                    case 8:
                        ShowSalesById(); 
                        break;
                    case 0:
                        Console.WriteLine("Going back to the main menu.");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (option != 0);
        }
        private static void ShowSales() { }
        private static void ReturnPurchase() { }
        private static void RemoveSales() { }
        private static void AddNewSales() { }
        private static void ShowSalesByDate() { }
        private static void ShowSalesByAmountRange() { }
        private static void SearchBySaleDate() { }
        private static void ShowSalesById() { }
    }
}
