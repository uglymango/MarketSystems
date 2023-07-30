using MarketManagement.HelpMenu;

namespace MarketManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
           //Here we create the main menu which gives us 3 options
            
            int option;

            do
            {
                Console.WriteLine("1. Manage products");
                Console.WriteLine("2. Manage sales");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("----------------");
                Console.WriteLine("Enter an option please:");
                Console.WriteLine("----------------");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter an option please:");
                    Console.WriteLine("----------------");
                }

                switch (option)
                {
                    case 1:
                        SubMenu.ManageProducts();
                        break;
                    case 2:
                        SubMenu.ManageSales();
                        break;
                    case 0:
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

                //The loop here stops if option is 0 (which is exit) and program ends
            } while (option != 0);


        }
    }
}