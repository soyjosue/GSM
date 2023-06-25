using GSM.Services.Customers.Domain.Models;
using GSM.Services.Customers.Shared.Dtos;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Customers.Services.EventHandlers.Commands;

public class EditCustomerEventCommand : IRequest<CommandGenericResult<Customer>>
{
    public EditCustomerEventCommand(Guid id, string names, string surnames)
    {
        Id = id;
        Names = names;
        Surnames = surnames;
    }

    public Guid Id { get; set; }
    public string Names { get; set; }
    public string Surnames { get; set; }
}