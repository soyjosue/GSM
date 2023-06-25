using System.ComponentModel.DataAnnotations;
using GSM.Shared.Models;

namespace GSM.Services.Security.Domain.Models;

public class LoginAttempt : BaseEntity
{
    public LoginAttempt(string username, string message, int result)
    {
        Username = username;
        Message = message;
        Result = result;
    }
    
    [Required]
    public string Username { get; set; }

    [Required]
    public string Message { get; set; }

    [Required]
    public int Result { get; set; }
}