using ConsoleTables;
using MarketConsole.Services.Concrete;
using MarketSystems.Data.Enums;
using MarketSystems.Data.Models;
using System;
using System.Linq;

namespace MarketManagement.HelpMenu
{
    public class ManageProducts
    {
        private readonly MarketService marketService;

        public ManageProducts(MarketService marketService)
        {
            this.marketService = marketService;
        }

        public ManageProducts()
        {
        }

        public void ShowProducts()
        {
            try
            {
                var products = marketService.GetProducts();
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
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while showing products: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public void AddNewProduct()
        {
            try
            {
                Console.Write("Enter the product name: ");
                string name = Console.ReadLine();

                Console.Write("Enter the product price: ");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.Write("Choose the product category: ");
                ProductCategory category = GetSelectedCategory();

                Console.Write("Enter the product quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                marketService.AddProduct(name, price, category, quantity);

                Console.WriteLine("Product added successfully!");
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding product: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public void UpdateProduct(int Id, string newName, decimal newPrice, ProductCategory newCategory, int newCounts)
        {
            try
            {
                // Check if the new price is not negative
                if (newPrice < 0)
                {
                    Console.WriteLine("Error: The new price cannot be negative.");
                    Console.WriteLine("------------------------");
                    return;
                }

                // Check if the product with the given ID exists
                var productToUpdate = marketService.GetProducts().FirstOrDefault(p => p.Id == Id);
                if (productToUpdate == null)
                {
                    Console.WriteLine("Error: Product with the given ID not found.");
                    Console.WriteLine("------------------------");
                    return;
                }

                marketService.UpdateProduct(Id, newName, newPrice, newCategory, newCounts);
                Console.WriteLine("Product updated successfully!");
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating product: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }


        public void RemoveProduct()
        {
            try
            {
                Console.Write("Enter the product ID to remove: ");
                int productId = int.Parse(Console.ReadLine());

                // Call the appropriate method from marketService to remove the product
                marketService.DeleteProduct(productId);

                Console.WriteLine("Product removed successfully!");
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while removing product: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public void ShowCategoryByProduct()
        {
            try
            {
                // Assume you have a method to display and choose from existing categories
                Console.Write("Choose the product category (0-7): ");
                int categoryNumber;
                while (!int.TryParse(Console.ReadLine(), out categoryNumber) ||
                       categoryNumber < 0 || categoryNumber > (int)ProductCategory.Books)
                {
                    Console.WriteLine("Invalid category selection. Please try again.");
                }

                ProductCategory category = (ProductCategory)categoryNumber;

                var productsInCategory = marketService.ShowCategoryByProduct(category);

                if (productsInCategory.Count == 0)
                {
                    Console.WriteLine($"No products found in the category: {category}");
                }
                else
                {
                    Console.WriteLine($"Products in the category: {category}");
                }
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while showing products by category: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        public void ShowProductByPriceRange()
        {
            try
            {
                Console.Write("Enter the minimum price: ");
                decimal minPrice = decimal.Parse(Console.ReadLine());

                Console.Write("Enter the maximum price: ");
                decimal maxPrice = decimal.Parse(Console.ReadLine());

                var productsByPriceRange = marketService.ShowProductByPriceRange(minPrice, maxPrice);

                if (productsByPriceRange.Count == 0)
                {
                    Console.WriteLine("No products found within the given price range.");
                }
                else
                {
                    Console.WriteLine("Products within the given price range:");
                    foreach (var product in productsByPriceRange)
                    {
                        Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Price: {product.Price} | Category: {product.Category} | Count: {product.Counts}");
                    }
                }

                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while showing products by price range: {ex.Message}");
                Console.WriteLine("------------------------");
            }

        }

        public void FindProductByName()
        {
            try
            {
                // Get the search text from the user
                Console.Write("Enter the product name to search: ");
                string searchText = Console.ReadLine();

                // Call the appropriate method from marketService to search products by name
                marketService.FindProductByName(searchText);
                Console.WriteLine("------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while searching products by name: {ex.Message}");
                Console.WriteLine("------------------------");
            }
        }

        private ProductCategory GetSelectedCategory()
        {           

            // This method can be modified as per your application's requirements.
            Console.WriteLine("Available Categories:");
            foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
            {
                Console.WriteLine($"{(int)category}. {category}");
            }

            int categoryNumber;
            while (!int.TryParse(Console.ReadLine(), out categoryNumber) ||
                   !Enum.IsDefined(typeof(ProductCategory), categoryNumber))
            {
                Console.WriteLine("Invalid category selection. Please try again.");
            }

            return (ProductCategory)categoryNumber;
        }

        internal void UpdateProduct()
        {
            throw new NotImplementedException();
        }
    }
}
