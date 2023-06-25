using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GSM.Shared.EnvironmentVariable;
using Microsoft.IdentityModel.Tokens;

namespace GSM.Shared.Setup.Jwt;

public static class JwtSecurityUtils
{
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
    
    public static string CreateToken(Guid accountId, string username)
    {
        JwtSecurityTokenHandler tokenHandler = CreateTokenHandler();

        byte[] key = GetSecretKey();
        List<Claim> claims = GetClaims(accountId, username);

        SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(claims: claims, key: key);
        SecurityToken securityToken = tokenHandler.CreateSecurityToken(tokenDescriptor: tokenDescriptor);

        return tokenHandler.GetJwt(securityToken);
    }

    private static JwtSecurityTokenHandler CreateTokenHandler() => new();
    private static byte[] GetSecretKey() => Encoding.UTF8.GetBytes(EnvironmentUtils.Get<string>("JWT_SETTINGS_KEY"));
    private static Claim CreateClaim(string type, string value) => new(type: type, value: value);
    private static List<Claim> GetClaims(Guid accountId, string username) => new()
    {
        CreateClaim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
        CreateClaim(type: "AccountId", value: accountId.ToString()),
        CreateClaim(type: "Username", value: username),
    };
    private static SecurityTokenDescriptor CreateTokenDescriptor(List<Claim> claims, byte[] key) =>
        new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifetime),
            Issuer = EnvironmentUtils.Get<string>("JWT_SETTINGS_ISSUER"),
            Claims = new Dictionary<string, object>()
            {
                {
                    JwtRegisteredClaimNames.Aud,
                    EnvironmentUtils.Get<string>("JWT_SETTINGS_AUDIENCE").Split(",")
                }
            },
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    private static SecurityToken CreateSecurityToken(this JwtSecurityTokenHandler tokenHandler,
        SecurityTokenDescriptor tokenDescriptor) => tokenHandler.CreateToken(tokenDescriptor);
    private static string GetJwt(this JwtSecurityTokenHandler tokenHandler, SecurityToken token)
        => tokenHandler.WriteToken(token);
}