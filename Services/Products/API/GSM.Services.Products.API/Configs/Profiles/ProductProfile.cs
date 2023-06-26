using AutoMapper;
using GSM.Services.Products.Domain.Models;
using GSM.Services.Products.Services.EventHandlers.Commands;
using GSM.Services.Products.Shared.Dtos;

namespace GSM.Services.Products.API.Configs.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductEventCommand, Product>();
        CreateMap<Product, ProductDto>();
    }
}