using GSM.Services.Security.Domain.Models;
using GSM.Shared.Setup.CQRS.Queries;

namespace GSM.Services.Security.Services.Queries.Interfaces;

public interface IAccountQueryService : IBaseQueryService<Account>
{
    Task<Account?> GetByUsernameAsync(string username);
}