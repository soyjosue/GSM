namespace GSM.Services.Security.Shared.Dtos;

public class AccountDto
{
    public AccountDto(string username, string names)
    {
        Username = username;
        Names = names;
    }
    
    public AccountDto(Guid id, string username, string names)
    {
        Id = id;
        Username = username;
        Names = names;
    }
    
    public AccountDto(Guid id, string username, string names, string surnames)
    {
        Id = id;
        Username = username;
        Names = names;
        Surnames = surnames;
    }
    
    public Guid Id { get; set; }

    public string Username { get; set; }
    
    public string Names { get; set; }
    
    public string? Surnames { get; set; }
}