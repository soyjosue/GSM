using Microsoft.EntityFrameworkCore;

namespace GSM.Shared.Setup.CQRS.Queries;

public interface IBaseQueryService<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}