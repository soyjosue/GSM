using GSM.Services.Products.Domain.Models;
using GSM.Shared.Setup.Database;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Products.Persistence.Database;

public class ProductDbContext : DefaultDbContext
{
    public ProductDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}