using GSM.Services.Customers.Domain.Models;
using GSM.Services.Customers.Persistence.Database;
using GSM.Services.Customers.Services.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Customers.Services.Queries;

public class CustomerQueryService : ICustomerQueryService
{
    private readonly DbSet<Customer> _dbSet;
    
    public CustomerQueryService(CustomerDbContext context)
    {
        _dbSet = context.Set<Customer>();
    }

    public Task<List<Customer>> GetAllAsync()
        => _dbSet.ToListAsync();

    public Task<Customer?> GetByIdAsync(Guid id)
        => _dbSet.FirstOrDefaultAsync(i => i.Id == id);

    public Task<bool> ExistsAsync(Guid id)
        => _dbSet.AnyAsync(i => i.Id == id);
}