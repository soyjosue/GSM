using System.ComponentModel.DataAnnotations;
using GSM.Services.Security.Domain.Models;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Security.Services.EventHandlers.Commands;

public class EditAccountEventCommand : IRequest<CommandGenericResult<Account>>
{
    public EditAccountEventCommand(Guid id, string names)
    {
        Id = id;
        Names = names;
    }
    
    public EditAccountEventCommand(Guid id, string names, string surnames)
    {
        Id = id;
        Names = names;
        Surnames = surnames;
    }

    [Required(ErrorMessage = "El ID de la cuenta es obligatorio.")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Names { get; set; }
    
    public string? Surnames { get; set; }
}