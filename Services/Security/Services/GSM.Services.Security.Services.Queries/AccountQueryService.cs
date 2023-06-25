using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Persistence.Database;
using GSM.Services.Security.Services.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Security.Services.Queries;

public class AccountQueryService : IAccountQueryService
{
    private readonly DbSet<Account> _dbSet;
    public AccountQueryService(SecurityDbContext context)
    {
        _dbSet = context.Set<Account>();
    }

    public Task<List<Account>> GetAllAsync()
        => _dbSet.ToListAsync();

    public Task<Account?> GetByIdAsync(Guid id)
        => _dbSet.FirstOrDefaultAsync(i => i.Id == id);

    public Task<bool> ExistsAsync(Guid id)
        => _dbSet.AnyAsync(i => i.Id == id);

    public Task<Account?> GetByUsernameAsync(string username)
        => _dbSet.FirstOrDefaultAsync(i => i.Username.ToLower() == username.ToLower());
}