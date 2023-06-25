using AutoMapper;
using GSM.Services.Customers.Domain.Models;
using GSM.Services.Customers.Services.EventHandlers.Commands;
using GSM.Services.Customers.Persistence.Database;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Customers.Services.EventHandlers.Handlers;

public class CreateCustomerEventHandler : IRequestHandler<CreateCustomerEventCommand, CommandGenericResult<Customer>>
{
    private readonly CustomerDbContext _context;
    private readonly DbSet<Customer> _dbSet;
    private readonly IMapper _mapper;

    public CreateCustomerEventHandler(CustomerDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<Customer>();
        _mapper = mapper;
    }

    public async Task<CommandGenericResult<Customer>> Handle(CreateCustomerEventCommand request,
        CancellationToken cancellationToken)
    {
        CommandGenericResult<Customer> commandResult 
            = new CommandGenericResult<Customer>();

        try
        {
            Customer customer = _mapper.Map<Customer>(request);
            
            customer.CreatedTime = DateTime.Now;

            if (await _dbSet.AnyAsync(i => i.IdentificationNumber.ToLower() == customer.IdentificationNumber.ToLower()))
            {
                commandResult.Message = $"El cliente con la identificación {customer.IdentificationNumber} se encuentra registrado.";
                return commandResult;
            }

            await _dbSet.AddAsync(
                customer
            );

            bool result = await _context.SaveChangesAsync() > 0;

            commandResult.IsSuccess = result;
            commandResult.Result = customer;
            commandResult.Message =
                result ? "El cliente fue creado exitosamente." : "El cliente no pudo ser creado, intente nuevamente.";

            return commandResult;
        }
        catch (Exception ex)
        {
            commandResult.Message = ex.Message;
            return commandResult;
        }
    }
}