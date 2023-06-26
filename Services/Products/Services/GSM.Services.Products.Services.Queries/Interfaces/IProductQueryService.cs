using GSM.Services.Products.Domain.Models;
using GSM.Shared.Setup.CQRS.Queries;

namespace GSM.Services.Products.Services.Queries.Interfaces;

public interface IProductQueryService : IBaseQueryService<Product>
{
    
}