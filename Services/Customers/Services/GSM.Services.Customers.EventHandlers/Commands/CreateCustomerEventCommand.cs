using System.ComponentModel.DataAnnotations;
using GSM.Services.Customers.Domain.Models;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Customers.EventHandlers.Commands;

public class CreateCustomerEventCommand : IRequest<CommandGenericResult<Customer>>
{
    public CreateCustomerEventCommand(string identificationNumber, string names, string surnames)
    {
        this.IdentificationNumber = identificationNumber;
        this.Names = names;
        this.Surnames = surnames;
    }
    
    [Required(ErrorMessage = "Numero de identificación obligatorio.")]
    public string IdentificationNumber { get; set; }
    
    [Required(ErrorMessage = "Los nombres son obligatorios.")]
    public string Names { get; set; }
    
    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    public string Surnames { get; set; }
}