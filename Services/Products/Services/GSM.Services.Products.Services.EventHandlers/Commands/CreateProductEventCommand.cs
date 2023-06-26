using GSM.Services.Products.Domain.Models;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Products.Services.EventHandlers.Commands;

public class CreateProductEventCommand : IRequest<CommandGenericResult<Product>>
{
    public CreateProductEventCommand(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}