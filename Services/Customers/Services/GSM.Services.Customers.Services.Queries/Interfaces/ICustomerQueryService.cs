using GSM.Services.Customers.Domain.Models;
using GSM.Shared.Setup.CQRS.Queries;

namespace GSM.Services.Customers.Services.Queries.Interfaces;

public interface ICustomerQueryService : IBaseQueryService<Customer> { }