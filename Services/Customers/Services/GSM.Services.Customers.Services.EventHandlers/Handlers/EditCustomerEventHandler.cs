using AutoMapper;
using GSM.Services.Customers.Domain.Models;
using GSM.Services.Customers.Services.EventHandlers.Commands;
using GSM.Services.Customers.Persistence.Database;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Customers.Services.EventHandlers.Handlers;

public class EditCustomerEventHandler : IRequestHandler<EditCustomerEventCommand, CommandGenericResult<Customer>>
{
    private readonly CustomerDbContext _context;
    private readonly DbSet<Customer> _dbSet;
    private readonly IMapper _mapper;
    
    public EditCustomerEventHandler(CustomerDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<Customer>();
        _mapper = mapper;
    }
    
    public async Task<CommandGenericResult<Customer>> Handle(EditCustomerEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<Customer> commandResult = new CommandGenericResult<Customer>();

        try
        {
            Customer? customer = await _dbSet.FirstOrDefaultAsync(i => i.Id == request.Id);
            
            if (customer == null)
            {
                commandResult.Message = "No se encontro el cliente el cual desea editar.";
                return commandResult;
            }

            customer.Names = request.Names;
            customer.Surnames = request.Surnames;
            customer.UpdateTime = DateTime.Now;

            _dbSet.Update(customer);

            commandResult.IsSuccess = await _context.SaveChangesAsync() > 0;
            commandResult.Message = commandResult.IsSuccess ? "El cliente fue editado exitosamente." : "El cliente no pudo ser editado.";
            commandResult.Result = customer;

            return commandResult;
        }
        catch (Exception ex)
        {
            commandResult.Message = ex.Message;
            return commandResult;
        }
    }
}