using GSM.Services.Customers.Domain.Models;
using GSM.Shared.Setup.Database;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Customers.Persistence.Database;

public class CustomerDbContext : DefaultDbContext
{
    
    public CustomerDbContext(DbContextOptions<DefaultDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    
}