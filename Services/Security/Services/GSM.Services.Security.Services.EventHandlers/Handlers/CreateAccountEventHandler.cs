using AutoMapper;
using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Persistence.Database;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Shared.Helpers;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GSM.Services.Security.Services.EventHandlers.Handlers;

public class CreateAccountEventHandler : IRequestHandler<CreateAccountEventCommand, CommandGenericResult<Account>>
{
    private readonly SecurityDbContext _context;
    private readonly DbSet<Account> _dbSet;
    private readonly IMapper _mapper;
    
    public CreateAccountEventHandler(SecurityDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<Account>();
        _mapper = mapper;
    }
    
    public async Task<CommandGenericResult<Account>> Handle(CreateAccountEventCommand request, CancellationToken cancellationToken)
    {
        CommandGenericResult<Account> commandResult = new CommandGenericResult<Account>();

        try
        {
            if (await _dbSet.AnyAsync(i => i.Username.ToLower() == request.Username.ToLower()))
            {
                commandResult.Message = $"Existe una cuenta con el nombre de usuario {request.Username}";
                return commandResult;
            }

            if (request.Password.Length < 8)
            {
                commandResult.Message = "La contraseña debe tener más de 8 caracteres.";
                return commandResult;
            }

            Account account = _mapper.Map<Account>(request);
            account.CreatedTime = DateTime.Now;
            account.IsActive = true;
            account.Password = CryptUtils.HashPassword(request.Password);

            await _dbSet.AddAsync(account);

            bool result = await _context.SaveChangesAsync() > 0;

            commandResult.IsSuccess = result;
            commandResult.Message =
                result ? "La cuenta fue registrada exitosamente." : "La cuenta no pudo ser registrada.";
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