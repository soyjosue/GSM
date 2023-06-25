using GSM.Services.Security.API.Extensions;
using GSM.Services.Security.Persistence.Database;
using GSM.Shared.Setup.API;
using GSM.Shared.Setup.AutoMapper;
using GSM.Shared.Setup.CQRS;
using GSM.Shared.Setup.Database.Service;

WebApplication app = DefaultWebApplication.Create(args, builder =>
{
    builder.Services.ConfigureQueryServices();
    
    builder.Services.ConfigureDatabase<SecurityDbContext>();
    
    builder.Services.ConfigureMediatR();
    
    builder.Services.ConfigureAutoMapper();
});

DefaultWebApplication.Run(app);