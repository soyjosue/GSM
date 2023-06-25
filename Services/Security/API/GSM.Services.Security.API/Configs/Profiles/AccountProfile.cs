using AutoMapper;
using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Services.Security.Shared.Dtos;
using GSM.Shared.Helpers;

namespace GSM.Services.Security.API.Configs.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<CreateAccountEventCommand, Account>()
            .ForMember(i => i.Password,
                        y => y.MapFrom(z => CryptUtils.HashPassword(z.Password)));

        CreateMap<Account, AccountDto>();
    }
}