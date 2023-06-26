using GSM.Services.Customers.API.Extensions;
using GSM.Services.Customers.Persistence.Database;
using GSM.Shared.Setup.API;
using GSM.Shared.Setup.AutoMapper;
using GSM.Shared.Setup.CQRS;
using GSM.Shared.Setup.Database.Service;

WebApplication app = DefaultWebApplication.Create(args, builder =>
{
    builder.Services.ConfigureQueryServices();
    
    builder.Services.ConfigureDatabase<CustomerDbContext>();

    builder.Services.ConfigureMediatR();
    
    builder.Services.ConfigureAutoMapper();
});

DefaultWebApplication.Run(app);