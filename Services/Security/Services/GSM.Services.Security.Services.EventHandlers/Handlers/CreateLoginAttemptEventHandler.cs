using AutoMapper;
using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Persistence.Database;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Security.Services.EventHandlers.Handlers;

public class CreateLoginAttemptEventHandler : IRequestHandler<CreateLoginAttemptEventCommand, CommandGenericResult<LoginAttempt>>
{
    private readonly SecurityDbContext _context;
    private readonly DbSet<LoginAttempt> _dbSet;
    private readonly IMapper _mapper;
    
    public CreateLoginAttemptEventHandler(SecurityDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<LoginAttempt>();
        _mapper = mapper;
    }
    
    public async Task<CommandGenericResult<LoginAttempt>> Handle(CreateLoginAttemptEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<LoginAttempt> commandResult = new CommandGenericResult<LoginAttempt>();

        try
        {

            LoginAttempt loginAttempt = _mapper.Map<LoginAttempt>(request);
            loginAttempt.CreatedTime = DateTime.Now;

            await _dbSet.AddAsync(loginAttempt);
            
            bool result = await _context.SaveChangesAsync() > 0;

            commandResult.IsSuccess = result;
            commandResult.Message = result ? "El intento de inicio de sesión fue guardado exitosamente." : "El intento de inicio de sesión no pudo ser guardado.";
            commandResult.Result = loginAttempt;

            return commandResult;
        }
        catch (Exception ex)
        {
            commandResult.Message = ex.Message;
            return commandResult;
        }
        
    }
}