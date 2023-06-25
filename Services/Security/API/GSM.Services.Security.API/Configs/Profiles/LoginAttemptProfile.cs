using AutoMapper;
using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Services.EventHandlers.Commands;

namespace GSM.Services.Security.API.Configs.Profiles;

public class LoginAttemptProfile : Profile
{
    public LoginAttemptProfile()
    {
        CreateMap<CreateLoginAttemptEventCommand, LoginAttempt>();
    }
}