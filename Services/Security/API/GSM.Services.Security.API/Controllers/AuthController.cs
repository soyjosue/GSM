using GSM.Services.Security.Domain.Models;
using GSM.Services.Security.Services.EventHandlers.Commands;
using GSM.Services.Security.Services.Queries.Interfaces;
using GSM.Services.Security.Shared.Dtos;
using GSM.Shared.Dtos;
using GSM.Shared.Setup.CQRS.Commands;
using GSM.Shared.Setup.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GSM.Services.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAccountQueryService _accountQueryService;
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, IAccountQueryService accountQueryService)
        {
            _mediator = mediator;
            _accountQueryService = accountQueryService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<LoginSuccessDto>>> LoginAsync(
            ValidAccountCredentialsEventCommand command)
        {
            Response<LoginSuccessDto> response = new Response<LoginSuccessDto>();

            try
            {
                CommandGenericResult<bool> commandResult = await _mediator.Send(command);

                if (commandResult.Result)
                {
                    Account? account = await _accountQueryService.GetByUsernameAsync(command.Username);

                    response.Data = new LoginSuccessDto(
                        accountId: account!.Id,
                        token: JwtSecurityUtils.CreateToken(account.Id, account.Username)
                    );
                }

                response.IsSuccess = commandResult.Result;
                response.Message = commandResult.Message;
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