﻿namespace ShopProductManager.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int Price { get; set; }

    public string CategoryName { get; set; }
}
