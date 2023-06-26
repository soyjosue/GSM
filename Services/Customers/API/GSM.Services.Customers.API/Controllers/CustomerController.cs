using AutoMapper;
using GSM.Services.Customers.Domain.Models;
using GSM.Services.Customers.Services.EventHandlers.Commands;
using GSM.Services.Customers.Services.Queries.Interfaces;
using GSM.Services.Customers.Shared.Dtos;
using GSM.Shared.Dtos;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSM.Services.Customers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerQueryService _customerQueryService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public CustomerController(ICustomerQueryService customerQueryService, IMapper mapper, IMediator mediator)
        {
            _customerQueryService = customerQueryService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<CustomerGetAllDto>>>> GetAllAsync()
        {
            Response<List<CustomerGetAllDto>> response = new Response<List<CustomerGetAllDto>>();

            try
            {
                List<Customer> entities = await _customerQueryService.GetAllAsync();
                
                response.IsSuccess = true;
                response.Message = "Clientes obtenidos exitosamente.";
                response.Data = _mapper.Map<List<CustomerGetAllDto>>(entities);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<CustomerGetByIdDto>>> GetByIdAsync(Guid id)
        {
            Response<CustomerGetByIdDto> response = new Response<CustomerGetByIdDto>();

            try
            {
                Customer? customer = await _customerQueryService.GetByIdAsync(id);

                if (customer == null)
                {
                    response.Message = "El cliente no fue encontrado.";
                    return NotFound(response);
                }
                
                response.IsSuccess = true;
                response.Message = "Cliente obtenido exitosamente.";
                response.Data = _mapper.Map<CustomerGetByIdDto>(customer);
                
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<CustomerDto>>> CreateAsync(CreateCustomerEventCommand command)
        {
            Response<CustomerDto> response = new Response<CustomerDto>();

            try
            {
                CommandGenericResult<Customer> commandResult =
                    await _mediator.Send(command);

                if (!commandResult.IsSuccess)
                {
                    response.Message = commandResult.Message;
                    return BadRequest(response);
                }
                
                response.IsSuccess = true;
                response.Message = "El cliente fue creado exitosamente.";
                response.Data = _mapper.Map<CustomerDto>(commandResult.Result);
                
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Response<CustomerDto>>> EditAsync(EditCustomerEventCommand command)
        {
            Response<CustomerDto> response = new Response<CustomerDto>();
            
            try
            {
                CommandGenericResult<Customer> commandResult = await _mediator.Send(command);

                if (!commandResult.IsSuccess)
                {
                    response.Message = commandResult.Message;
                    return BadRequest(response);
                }

                response.IsSuccess = true;
                response.Message = "El cliente fue creado exitosamente.";
                response.Data = _mapper.Map<CustomerDto>(commandResult.Result);
                
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
