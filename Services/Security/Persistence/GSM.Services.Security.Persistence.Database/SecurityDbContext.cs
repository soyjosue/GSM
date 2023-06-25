using GSM.Services.Security.Domain.Models;
using GSM.Shared.Setup.Database;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Security.Persistence.Database;

public class SecurityDbContext : DefaultDbContext
{
    public SecurityDbContext(DbContextOptions<DefaultDbContext> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<LoginAttempt> LoginAttempts => Set<LoginAttempt>();
}