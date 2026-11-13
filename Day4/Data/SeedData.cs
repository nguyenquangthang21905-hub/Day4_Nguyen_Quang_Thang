using Day4.Models;
namespace Day4.Data
{
    public class SeedData
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Electronics" },
            new Category { CategoryId = 2, CategoryName = "Clothing" },
            new Category { CategoryId = 3, CategoryName = "Books" }
        };
        public static List<Product> Products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Smartphone", Price = 699.99m, CategoryId = 1 },
            new Product { ProductId = 2, ProductName = "Laptop", Price = 999.99m, CategoryId = 1 },
            new Product { ProductId = 3, ProductName = "T-Shirt", Price = 19.99m, CategoryId = 2 },
            new Product { ProductId = 4, ProductName = "Jeans", Price = 49.99m, CategoryId = 2 },
            new Product { ProductId = 5, ProductName = "Novel", Price = 14.99m, CategoryId = 3 }
        };
    }
}

