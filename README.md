# ShopProductManager 🛒

A simple C# console application for managing products in a SQL Server database using Dapper.NET.

## Features

- Add new products
- Edit existing products
- Delete products
- List all products with their category names
- Input validation (length, format, numeric ranges, etc.)
- Clean UI with looping menu and clear feedback messages

## Technologies Used

- C# (.NET)
- Dapper.NET
- SQL Server
- Console UI

## ِatabase Schema

- Categories
- Customers
- Products (with `CategoryId` as foreign key)
- Orders

> Product `Id` is auto-generated with `IDENTITY(1,1)`.

## How to Run

1. Clone the repo:
   bash
   git clone https://github.com/YOUR_USERNAME/ShopProductManager.git


2. Open the project in Visual Studio or VS Code.

3. Make sure SQL Server is running and ShopDb is created using provided script.

4. Run the application:
dotnet run

Sample Category IDs
ID	Name
1. Electronics
2. Accessories
3. Peripherals
4. Clothing

Notes
This app is built as a learning project for Dapper and SQL operations.

Input validations are implemented to make the experience smooth and bug-free.

## Author

Made with ❤️ by [goat-debug](https://github.com/goat-debug)

> If you found this project helpful, feel free to ⭐ star the repository!
