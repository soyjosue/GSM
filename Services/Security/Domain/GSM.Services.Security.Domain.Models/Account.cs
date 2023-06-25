using System.ComponentModel.DataAnnotations;
using GSM.Shared.Helpers;
using GSM.Shared.Models;

namespace GSM.Services.Security.Domain.Models;

public class Account : BaseEntity
{
    public Account(string username, string names, string surnames, string password)
    {
        Username = username;
        Names = names;
        Surnames = surnames;
        Password = password;
    }
    
    public Account(string username, string names, string password)
    {
        Username = username;
        Names = names;
        Password = password;
    }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Names { get; set; }
    
    public string? Surnames { get; set; }

    [Required]
    public string Password { get; set; }
}