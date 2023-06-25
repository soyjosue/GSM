namespace GSM.Services.Security.Shared.Dtos;

public class LoginSuccessDto
{
    public LoginSuccessDto(Guid accountId, string token)
    {
        AccountId = accountId;
        Token = token;
    }
    
    public Guid AccountId { get; set; }
    public string Token { get; set; }
}