using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Persistence.Database;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Security.Services.EventHandlers.Handlers;

public class EditAccountEventHandler : IRequestHandler<EditAccountEventCommand, CommandGenericResult<Account>>
{
    private readonly SecurityDbContext _context;
    private readonly DbSet<Account> _dbSet;

    public EditAccountEventHandler(SecurityDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Account>();
    }

    public async Task<CommandGenericResult<Account>> Handle(EditAccountEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<Account> commandResult = new CommandGenericResult<Account>();

        try
        {
            Account? account = await _dbSet.FirstOrDefaultAsync(i => i.Id == request.Id);

            if (account == null)
            {
                commandResult.Message = "La cuenta que desea editar no fue encontrada.";
                return commandResult;
            }

            account.Names = request.Names;
            account.Surnames = request.Surnames;
            account.UpdateTime = DateTime.Now;

            _dbSet.Update(account);

            bool result = await _context.SaveChangesAsync() > 0;

            commandResult.IsSuccess = result;
            commandResult.Message = result ? "La cuenta fue editada exitosamente." : "La cuenta no pudo ser editada.";
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