using ShopProductManager.Services;

while (true)
{
    Console.Clear();
    Console.WriteLine("==== Shop Product Manager ====\n");

    ProductService.ShowAll();

    Console.WriteLine("\nOptions:");
    Console.WriteLine("1. Add product");
    Console.WriteLine("2. Edit product");
    Console.WriteLine("3. Delete product");
    Console.WriteLine("0. Exit");

    Console.Write("\nEnter your choice: ");
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            ProductService.Add();
            break;
        case "2":
            ProductService.Edit();
            break;
        case "3":
            ProductService.Delete();
            break;
        case "0":
            if (AskYesNo("Are you sure you want to exit?") == "y")
            {
                Console.WriteLine("Goodbye 👋");
                return;
            }
            break;
        default:
            Console.WriteLine("❌ Invalid option.");
            break;
    }

    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
}

string AskYesNo(string message)
{
    while (true)
    {
        Console.Write($"{message} (y/n): ");
        var input = Console.ReadLine()?.Trim().ToLower();

        if (input == "y" || input == "n")
            return input;

        Console.WriteLine("❌ Invalid input. Please enter 'y' or 'n'.");
    }
}
