using System.ComponentModel.DataAnnotations;
using GSM.Services.Security.Domain.Models;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Security.Services.EventHandlers.Commands;

public class CreateLoginAttemptEventCommand : IRequest<CommandGenericResult<LoginAttempt>>
{
    public CreateLoginAttemptEventCommand(string username, string message, int result)
    {
        Username = username;
        Message = message;
        Result = result;
    }
    
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "El mensaje es obligatorio.")]
    public string Message { get; set; }
    
    [Required(ErrorMessage = "El resultado es obligatorio.")]
    public int Result { get; set; }
}