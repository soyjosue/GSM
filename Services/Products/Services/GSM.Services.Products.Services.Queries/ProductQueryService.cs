using AutoMapper;
using GSM.Services.Products.Domain.Models;
using GSM.Services.Products.Persistence.Database;
using GSM.Services.Products.Services.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Products.Services.Queries;

public class ProductQueryService : IProductQueryService
{
    private readonly DbSet<Product> _dbSet;

    public ProductQueryService(ProductDbContext context)
    {
        _dbSet = context.Set<Product>();
    }

    public Task<List<Product>> GetAllAsync()
        => _dbSet.ToListAsync();

    public Task<Product?> GetByIdAsync(Guid id)
        => _dbSet.FirstOrDefaultAsync(i => i.Id == id);

    public Task<bool> ExistsAsync(Guid id)
        => _dbSet.AnyAsync(i => i.Id == id);
}