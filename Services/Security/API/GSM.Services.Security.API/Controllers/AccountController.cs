using AutoMapper;
using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Services.Security.Services.Queries.Interfaces;
using GSM.Services.Security.Shared.Dtos;
using GSM.Shared.Dtos;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSM.Services.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAccountQueryService _accountQueryService;
        
        public AccountController(IMediator mediator, IMapper mapper, IAccountQueryService accountQueryService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _accountQueryService = accountQueryService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<AccountDto>>>> GetAllAsync()
        {
            Response<List<AccountDto>> response = new Response<List<AccountDto>>();

            try
            {
                List<Account> accounts = await _accountQueryService.GetAllAsync(); 
                
                response.IsSuccess = true;
                response.Message = "Cuentas obtenidas exitosamente.";
                response.Data = _mapper.Map<List<AccountDto>>(accounts);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<AccountDto>>> GetByIdAsync(Guid id)
        {
            Response<AccountDto> response = new Response<AccountDto>();

            try
            {
                Account? account = await _accountQueryService.GetByIdAsync(id);

                if (account == null)
                {
                    response.Message = "La cuenta no fue encontrada.";
                    return NotFound(response);
                }
                
                response.IsSuccess = true;
                response.Message = "Cuenta obtenida exitosamente.";
                response.Data = _mapper.Map<AccountDto>(account);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<AccountDto>>> CreateAsync(CreateAccountEventCommand command)
        {
            Response<AccountDto> response = new Response<AccountDto>();

            try
            {
                CommandGenericResult<Account> commandResult = await _mediator.Send(command);

                if (!commandResult.IsSuccess)
                {
                    response.Message = commandResult.Message;
                    return BadRequest(response);
                }

                response.IsSuccess = commandResult.IsSuccess;
                response.Message = commandResult.Message;
                response.Data = _mapper.Map<AccountDto>(commandResult.Result);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<AccountDto>>> EditAsync(EditAccountEventCommand command)
        {
            Response<AccountDto> response = new Response<AccountDto>();

            try
            {
                CommandGenericResult<Account> commandResult = await _mediator.Send(command);

                if (!commandResult.IsSuccess)
                {
                    response.Message = commandResult.Message;
                    return BadRequest(response);
                }

                response.IsSuccess = true;
                response.Message = commandResult.Message;
                response.Data = _mapper.Map<AccountDto>(commandResult.Result);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("changePassword")]
        public async Task<ActionResult<Response<AccountDto>>> ChangePasswordAsync(ChangePasswordEventCommand command)
        {
            Response<AccountDto> response = new Response<AccountDto>();

            try
            {
                CommandGenericResult<Account> commandResult = await _mediator.Send(command);

                if (!commandResult.IsSuccess)
                {
                    response.Message = commandResult.Message;
                    return BadRequest(response);
                }

                response.IsSuccess = commandResult.IsSuccess;
                response.Message = commandResult.Message;
                response.Data = _mapper.Map<AccountDto>(commandResult.Result);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
