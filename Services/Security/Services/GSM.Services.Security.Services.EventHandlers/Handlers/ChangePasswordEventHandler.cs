using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Persistence.Database;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Shared.Helpers;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Security.Services.EventHandlers.Handlers;

public class ChangePasswordEventHandler : IRequestHandler<ChangePasswordEventCommand, CommandGenericResult<Account>>
{
    private readonly SecurityDbContext _context;
    private readonly DbSet<Account> _dbSet;
    private readonly IMediator _mediator;
    
    public ChangePasswordEventHandler(SecurityDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
        _dbSet = _context.Set<Account>();
    }
    
    public async Task<CommandGenericResult<Account>> Handle(ChangePasswordEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<Account> commandResult = new CommandGenericResult<Account>();

        try
        {
            if (request.Password.Length < 8)
            {
                commandResult.Message = "La contraseña debe tener más de 8 caracteres.";
                return commandResult;
            }
            
            Account? account = await _dbSet.FirstOrDefaultAsync(i => i.Id == request.Id);

            if (account == null)
            {
                commandResult.Message = "La cuenta no fue encontrada.";
                return commandResult;
            }

            CommandGenericResult<bool> validCredentialResult =
                await _mediator.Send(new ValidAccountCredentialsEventCommand(
                    username: account.Username,
                    password: request.OldPassword
                    ));

            if (!validCredentialResult.Result)
            {
                commandResult.Message = validCredentialResult.Message;
                return commandResult;
            }

            account.Password = CryptUtils.HashPassword(request.Password);
            account.UpdateTime = DateTime.Now;

            _dbSet.Update(account);
            
            bool result = await _context.SaveChangesAsync() > 0;

            commandResult.IsSuccess = result;
            commandResult.Message =
                result ? "La contraseña fue cambiada exitosamente." : "La contraseña no pudo ser cambiada.";
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