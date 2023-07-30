using MarketConsole.Data.Models;
using MarketConsole.Services.Concrete;
using MarketConsole.Services.Concrete;
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
                Console.WriteLine("0. Back to main menu.");
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
                        MenuService.ShowProducts();
                        break;
                    case 2:
                        MenuService.AddProduct();
                        break;
                    case 3:
                        MenuService.UpdateProduct();
                        break;
                    case 4:
                        MenuService.RemoveProduct();
                        break;
                    case 5:
                        MenuService.ShowCategoryByProduct();
                        break;
                    case 6:
                        MenuService.ShowProductByPriceRange();
                        break;
                    case 7:
                        MenuService.FindProductByName();
                        break;
                    case 0:
                        Console.WriteLine("Bye");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

            } while (option != 0);

        }

        public static void ManageSales()
        {

            int option;

            do
            {

                Console.WriteLine("1. Show sales.");
                Console.WriteLine("2. Add new sales.");
                Console.WriteLine("3. Remove product from sale.");
                Console.WriteLine("4. Return purchase.");
                Console.WriteLine("5. Show sales by date.");
                Console.WriteLine("6. Show sales by amount range.");
                Console.WriteLine("7. Showing sales on a given date.");
                Console.WriteLine("8. Show sales by ID.");
                Console.WriteLine("0. Back to main menu.");
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
                        MenuService.ShowSales();
                        break;
                    case 2:
                        MenuService.AddNewSale();
                        break;
                    case 3:
                        MenuService.RemoveProductFromSale();
                        break;
                    case 4:
                       MenuService.ReturnPurchase();
                        break;
                    case 5:
                        MenuService.ShowSalesByDate();
                        break;
                    case 6:
                        MenuService.ShowSalesByPriceRange();
                        break;
                    case 7:
                        MenuService.ShowSalesOnExactDate();
                        break;
                    case 8:
                        MenuService.ShowSalesByID();
                        break;
                    case 0:
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }


            } 
            while (option != 0);
        }

    }
}