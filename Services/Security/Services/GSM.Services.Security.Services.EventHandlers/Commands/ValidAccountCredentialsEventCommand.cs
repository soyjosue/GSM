using System.ComponentModel.DataAnnotations;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Security.Services.EventHandlers.Commands;

public class ValidAccountCredentialsEventCommand : IRequest<CommandGenericResult<bool>>
{
    public ValidAccountCredentialsEventCommand(string username, string password)
    {
        Username = username.Trim();
        Password = password;
    }
    
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; }
}