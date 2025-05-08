using Dapper;
using ShopProductManager.Data;
using ShopProductManager.Models;

namespace ShopProductManager.Services;

public static class ProductService
{
    public static void ShowAll()
    {
        using var connection = Db.GetConnection();

        var sql = @"
            SELECT 
                p.Id, p.Name, p.Price, p.CategoryId,
                c.Name AS CategoryName
            FROM Products p
            JOIN Categories c ON p.CategoryId = c.Id";

        var products = connection.Query<Product>(sql);

        Console.WriteLine("Product List:\n");

        foreach (var product in products)
        {
            Console.WriteLine($"[{product.Id}] {product.Name} - {product.Price}T ({product.CategoryName})");
        }
    }

    public static void Add()
    {
        using var conn = Db.GetConnection();

        string name;
        while (true)
        {
            Console.Write("Product Name (max 50 characters): ");
            name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                continue;
            }

            if (name.Length > 50)
            {
                Console.WriteLine("Name must be 50 characters or less.");
                continue;
            }

            if (!name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                Console.WriteLine("Name can only contain letters and spaces.");
                continue;
            }

            break;
        }

        Console.WriteLine("\nAvailable Categories:");
        Console.WriteLine("[1] Electronics");
        Console.WriteLine("[2] Accessories");
        Console.WriteLine("[3] Peripherals");
        Console.WriteLine("[4] Clothing");

        int categoryId;
        while (true)
        {
            Console.Write("Select Category ID (1 to 4): ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out categoryId) && categoryId >= 1 && categoryId <= 4)
                break;

            Console.WriteLine("Invalid category ID. Category ID must be between 1 and 4.");
        }

        int price;
        while (true)
        {
            Console.Write("Price (numeric, max 99999): ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out price))
            {
                if (price <= 99999)
                    break;
                else
                    Console.WriteLine("Price must not exceed 99999.");
            }
            else
            {
                Console.WriteLine("Invalid price. Please enter a number.");
            }
        }

        var sql = @"INSERT INTO Products (Name, CategoryId, Price)
                VALUES (@Name, @CategoryId, @Price)";

        var rows = conn.Execute(sql, new { Name = name, CategoryId = categoryId, Price = price });

        Console.WriteLine(rows > 0
            ? "Product added successfully."
            : "Failed to add product.");
    }

    public static void Edit()
    {
        using var conn = Db.GetConnection();

        Console.Write("Enter Product ID to edit: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var existing = conn.QueryFirstOrDefault<Product>(
            "SELECT * FROM Products WHERE Id = @Id", new { Id = id });

        if (existing == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        string name;
        while (true)
        {
            Console.Write($"New Name (leave empty to keep '{existing.Name}'): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                name = existing.Name;
                break;
            }

            if (input.Length > 50)
            {
                Console.WriteLine("Name must be 50 characters or less.");
                continue;
            }

            if (!input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                Console.WriteLine("Name can only contain letters and spaces.");
                continue;
            }

            name = input;
            break;
        }

        int categoryId;
        while (true)
        {
            Console.Write($"New Category ID (leave empty to keep '{existing.CategoryId}'): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                categoryId = existing.CategoryId;
                break;
            }

            if (int.TryParse(input, out categoryId))
                break;

            Console.WriteLine("❌ Invalid category ID.");
        }

        int price;
        while (true)
        {
            Console.Write($"New Price (leave empty to keep '{existing.Price}'): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                price = existing.Price;
                break;
            }

            if (int.TryParse(input, out price))
            {
                if (price <= 99999)
                    break;
                else
                    Console.WriteLine("❌ Price must not exceed 99999.");
            }
            else
            {
                Console.WriteLine("❌ Invalid price. Please enter a number.");
            }
        }

        var updateSql = @"UPDATE Products
                      SET Name = @Name, CategoryId = @CategoryId, Price = @Price
                      WHERE Id = @Id";

        var rows = conn.Execute(updateSql, new { Name = name, CategoryId = categoryId, Price = price, Id = id });

        Console.WriteLine(rows > 0
            ? "Product updated successfully."
            : "Failed to update product.");
    }

    public static void Delete()
    {
        using var conn = Db.GetConnection();

        Console.Write("Enter Product ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var existing = conn.QueryFirstOrDefault<Product>(
            @"SELECT p.Id, p.Name, p.Price, p.CategoryId, c.Name AS CategoryName
          FROM Products p
          JOIN Categories c ON p.CategoryId = c.Id
          WHERE p.Id = @Id", new { Id = id });

        if (existing == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine($"\nProduct Found: [{existing.Id}] {existing.Name} - {existing.Price}$ ({existing.CategoryName})");

        while (true)
        {
            Console.Write("Are you sure you want to delete this product? (y/n): ");
            var confirm = Console.ReadLine()?.Trim().ToLower();
            if (confirm == "n")
            {
                Console.WriteLine("Deletion cancelled.");
                return;
            }
            else if (confirm == "y")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        var sql = "DELETE FROM Products WHERE Id = @Id";
        var rows = conn.Execute(sql, new { Id = id });

        Console.WriteLine(rows > 0
            ? "Product deleted successfully."
            : "Failed to delete product.");
    }

}
