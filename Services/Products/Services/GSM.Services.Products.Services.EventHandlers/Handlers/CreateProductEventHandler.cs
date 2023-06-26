using AutoMapper;
using GSM.Services.Products.Domain.Models;
using GSM.Services.Products.Persistence.Database;
using GSM.Services.Products.Services.EventHandlers.Commands;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Products.Services.EventHandlers.Handlers;

public class CreateProductEventHandler : IRequestHandler<CreateProductEventCommand, CommandGenericResult<Product>>
{
    private readonly ProductDbContext _context;
    private readonly DbSet<Product> _dbSet;
    private readonly IMapper _mapper;
    
    public CreateProductEventHandler(ProductDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = _context.Set<Product>();
    }
    
    public async Task<CommandGenericResult<Product>> Handle(CreateProductEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<Product> commandResult = new();

        try
        {
            Product account = _mapper.Map<Product>(request);
            
            account.IsActive = true;
            account.CreatedTime = DateTime.Now;

            await _dbSet.AddAsync(account);
            bool result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                commandResult.Message = "El product no pudo ser creado.";
                return commandResult;
            }
            
            commandResult.IsSuccess = result;
            commandResult.Message = "El producto fue creado exitosamente.";
            commandResult.Result = account;
            
            return commandResult;
        }
        catch (Exception ex)
        {
            commandResult.Message = ex.Message;
            return commandResult;
        }
    }
}