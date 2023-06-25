using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Persistence.Database;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Shared.Helpers;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Security.Services.EventHandlers.Handlers;

public class ValidAccountCredentialsEventHandler : IRequestHandler<ValidAccountCredentialsEventCommand, CommandGenericResult<bool>>
{
    private readonly DbSet<Account> _accountDbSet;
    private readonly IMediator _mediator;
    public ValidAccountCredentialsEventHandler(SecurityDbContext context, IMediator mediator)
    {
        _mediator = mediator;
        _accountDbSet = context.Set<Account>();
    }
    
    public async Task<CommandGenericResult<bool>> Handle(ValidAccountCredentialsEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<bool> commandResult = new CommandGenericResult<bool>();

        try
        {
            Account? account = await _accountDbSet
                .FirstOrDefaultAsync(i => i.Username.ToLower() == request.Username.ToLower());

            if (account == null)
            {
                commandResult.Message = "La cuenta no fue encontrada.";
                await _mediator.Send(new CreateLoginAttemptEventCommand(
                    username: request.Username,
                    message: commandResult.Message,
                    result: 4
                    ));
                return commandResult;
            }

            if (!account.IsActive)
            {
                commandResult.Message = "La cuenta se encuentra inactiva.";
                await _mediator.Send(new CreateLoginAttemptEventCommand(
                    username: request.Username,
                    message: commandResult.Message,
                    result: 3
                ));
                return commandResult;
            }

            bool verifyPassword = CryptUtils.Verify(request.Password, account.Password);

            commandResult.IsSuccess = verifyPassword;
            commandResult.Message = verifyPassword ? "Inicio de Sesión exitoso." : "Usuario o Contraseña incorrecta.";
            commandResult.Result = verifyPassword;
            
            await _mediator.Send(new CreateLoginAttemptEventCommand(
                username: request.Username,
                message: commandResult.Message,
                result: commandResult.IsSuccess ? 1 : 2
            ));
            
            return commandResult;
        }
        catch (Exception ex)
        {
            commandResult.Message = ex.Message;
            return commandResult;
        }
    }
}