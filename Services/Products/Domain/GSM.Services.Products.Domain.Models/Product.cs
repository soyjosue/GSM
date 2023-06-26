using GSM.Shared.Models;

namespace GSM.Services.Products.Domain.Models;

public class Product : BaseEntity
{
    public Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}