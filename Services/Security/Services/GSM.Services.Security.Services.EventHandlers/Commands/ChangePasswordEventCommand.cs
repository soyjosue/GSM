using System.ComponentModel.DataAnnotations;
using GSM.Services.Security.Domain.Models;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Security.Services.EventHandlers.Commands;

public class ChangePasswordEventCommand : IRequest<CommandGenericResult<Account>>
{
    public ChangePasswordEventCommand(Guid id, string password, string oldPassword)
    {
        Id = id;
        Password = password;
        OldPassword = oldPassword;
    }

    [Required(ErrorMessage = "El ID de la cuenta es obligatorio.")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "La nueva contraseña es obligatorio.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "La vieja contraseña es obligatorio..")]
    public string OldPassword { get; set; }
}