using GSM.Services.Products.Domain.Models;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Products.Services.EventHandlers.Commands;

public class EditProductEventCommand : IRequest<CommandGenericResult<Product>>
{
    public EditProductEventCommand(Guid id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}