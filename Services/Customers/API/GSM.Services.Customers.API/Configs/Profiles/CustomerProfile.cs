using AutoMapper;
using GSM.Services.Customers.Domain.Models;
using GSM.Services.Customers.EventHandlers.Commands;
using GSM.Services.Customers.Shared.Dtos;

namespace GSM.Services.Customers.API.Configs.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerGetAllDto>();
        CreateMap<Customer, CustomerGetByIdDto>();
        
        CreateMap<CreateCustomerEventCommand, Customer>();
    }
}