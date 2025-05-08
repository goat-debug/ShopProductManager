
--کوئری 1: نمایش نام و قیمت محصولاتی که قیمتشان بیش از ۵۰۰ تومان است

SELECT Name, Price
FROM Products
WHERE Price > 500;

--کوئری 2: مجموع مبلغ سفارشات ثبت‌شده در هر سال

SELECT 
  YEAR(OrderDate) AS OrderYear, 
  SUM(TotalAmount) AS TotalSales
FROM Orders
GROUP BY YEAR(OrderDate)
ORDER BY OrderYear;

--کوئری 3: مجموع فروش برای هر دسته‌بندی محصول

SELECT 
  c.Name AS CategoryName,
  SUM(o.TotalAmount) AS TotalSales
FROM Orders o
JOIN Products p ON o.ProductId = p.Id
JOIN Categories c ON p.CategoryId = c.Id
GROUP BY c.Name
ORDER BY TotalSales DESC;

--کوئری 4: نمایش نام و قیمت محصولات به ترتیب قیمت از بالا به پایین

SELECT Name, Price
FROM Products
ORDER BY Price DESC;

--کوئری 5: تعداد سفارشاتی که هر مشتری ثبت کرده است

SELECT 
  c.Name AS CustomerName,
  COUNT(o.Id) AS OrderCount
FROM Customers c
LEFT JOIN Orders o ON c.Id = o.CustomerId
GROUP BY c.Name
ORDER BY OrderCount DESC;

--کوئری 6: میانگین قیمت محصولات در هر دسته‌بندی

SELECT 
  c.Name AS CategoryName,
  AVG(p.Price) AS AveragePrice
FROM Products p
JOIN Categories c ON p.CategoryId = c.Id
GROUP BY c.Name
ORDER BY AveragePrice DESC;

--کوئری 7: نمایش نام محصولات و نام دسته‌بندی آن‌ها به ترتیب الفبایی

SELECT 
  p.Name AS ProductName,
  c.Name AS CategoryName
FROM Products p
JOIN Categories c ON p.CategoryId = c.Id
ORDER BY p.Name ASC;

--کوئری 8: مجموع مبلغ سفارشات ثبت‌شده در هر دسته‌بندی در سال 2023

SELECT 
  c.Name AS CategoryName,
  SUM(o.TotalAmount) AS TotalSales_2023
FROM Orders o
JOIN Products p ON o.ProductId = p.Id
JOIN Categories c ON p.CategoryId = c.Id
WHERE YEAR(o.OrderDate) = 2023
GROUP BY c.Name
ORDER BY TotalSales_2023 DESC;

--کوئری 9: تعداد سفارشاتی که در هر ماه از سال ثبت شده است

SELECT 
  YEAR(OrderDate) AS OrderYear,
  MONTH(OrderDate) AS OrderMonth,
  COUNT(*) AS OrderCount
FROM Orders
GROUP BY YEAR(OrderDate), MONTH(OrderDate)
ORDER BY OrderYear, OrderMonth;

--کوئری 10: مجموع فروش (TotalAmount) برای هر مشتری

SELECT 
  c.Name AS CustomerName,
  SUM(o.TotalAmount) AS TotalSpent
FROM Customers c
JOIN Orders o ON c.Id = o.CustomerId
GROUP BY c.Name
ORDER BY TotalSpent DESC;

--کوئری 11: نام دسته‌بندی و تعداد سفارشات ثبت‌شده در هر دسته‌بندی

SELECT 
  c.Name AS CategoryName,
  COUNT(o.Id) AS OrderCount
FROM Orders o
JOIN Products p ON o.ProductId = p.Id
JOIN Categories c ON p.CategoryId = c.Id
GROUP BY c.Name
ORDER BY OrderCount DESC;

--کوئری 12: نام مشتری و تعداد سفارشات، به ترتیب تعداد از زیاد به کم

SELECT 
  c.Name AS CustomerName,
  COUNT(o.Id) AS OrderCount
FROM Customers c
JOIN Orders o ON c.Id = o.CustomerId
GROUP BY c.Name
ORDER BY OrderCount DESC;


--کوئری 13: تعداد سفارشات ثبت‌شده در هر سال

SELECT 
  YEAR(OrderDate) AS OrderYear,
  COUNT(*) AS OrderCount
FROM Orders
GROUP BY YEAR(OrderDate)
ORDER BY OrderYear;

--کوئری 14: نام محصولات و تعداد خریداران هر محصول، از زیاد به کم

SELECT 
  p.Name AS ProductName,
  COUNT(DISTINCT o.CustomerId) AS BuyerCount
FROM Products p
JOIN Orders o ON p.Id = o.ProductId
GROUP BY p.Name
ORDER BY BuyerCount DESC;