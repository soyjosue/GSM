using GSM.Services.Products.API.Extensions;
using GSM.Services.Products.Persistence.Database;
using GSM.Shared.Setup.API;
using GSM.Shared.Setup.AutoMapper;
using GSM.Shared.Setup.CQRS;
using GSM.Shared.Setup.Database.Service;

WebApplication app = DefaultWebApplication.Create(args, builder =>
{
    
    builder.Services.ConfigureQueryService();
    
    builder.Services.ConfigureDatabase<ProductDbContext>();
    
    builder.Services.ConfigureMediatR();
    
    builder.Services.ConfigureAutoMapper();
    
});

DefaultWebApplication.Run(app);