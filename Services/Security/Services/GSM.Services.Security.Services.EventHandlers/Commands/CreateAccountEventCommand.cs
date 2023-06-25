using System.ComponentModel.DataAnnotations;
using GSM.Services.Security.Domain.Models;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;

namespace GSM.Services.Security.Services.EventHandlers.Commands;

public class CreateAccountEventCommand : IRequest<CommandGenericResult<Account>>
{
    public CreateAccountEventCommand(string username, string names, string surnames, string password)
    {
        Username = username.Trim();
        Names = names;
        Surnames = surnames;
        Password = password;
    }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "El nombre de la cuenta es obligatorio.")]
    public string Names { get; set; }

    public string? Surnames { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; }
}