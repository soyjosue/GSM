using AutoMapper;
using GSM.Services.Products.Domain.Models;
using GSM.Services.Products.Persistence.Database;
using GSM.Services.Products.Services.EventHandlers.Commands;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Products.Services.EventHandlers.Handlers;

public class EditProductEventHandler : IRequestHandler<EditProductEventCommand, CommandGenericResult<Product>>
{
    private readonly ProductDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public EditProductEventHandler(ProductDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
    }
    
    public async Task<CommandGenericResult<Product>> Handle(EditProductEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<Product> commandResult = new CommandGenericResult<Product>();

        try
        {
            Product? product = await _dbSet.FirstOrDefaultAsync(i => i.Id == request.Id);

            if (product == null)
            {
                commandResult.Message = "El producto no fue encontrado.";
                return commandResult;
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            _dbSet.Update(product);
            bool result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                commandResult.Message = "El producto no pudo ser editado.";
                return commandResult;
            }

            commandResult.IsSuccess = true;
            commandResult.Message = "Producto editado exitosamente.";
            commandResult.Result = product;
            
            return commandResult;
        }
        catch (Exception ex)
        {
            commandResult.Message = ex.Message;
            return commandResult;
        }
    }
}