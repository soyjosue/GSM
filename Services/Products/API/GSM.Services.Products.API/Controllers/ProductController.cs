using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GSM.Services.Products.Domain.Models;
using GSM.Services.Products.Services.EventHandlers.Commands;
using GSM.Services.Products.Services.Queries.Interfaces;
using GSM.Services.Products.Shared.Dtos;
using GSM.Shared.Dtos;
using GSM.Shared.Setup.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSM.Services.Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public ProductController(IProductQueryService productQueryService, IMapper mapper, IMediator mediator)
        {
            _productQueryService = productQueryService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<ProductDto>>>> GetAllAsync()
        {
            Response<List<ProductDto>> response = new Response<List<ProductDto>>();

            try
            {
                List<Product> products = await _productQueryService.GetAllAsync();

                response.IsSuccess = true;
                response.Message = "Productos obtenidos exitosamente.";
                response.Data = _mapper.Map<List<ProductDto>>(products);

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ProductDto>>> GetByIdAsync(Guid id)
        {
            Response<ProductDto> response = new Response<ProductDto>();

            try
            {
                Product? product = await _productQueryService.GetByIdAsync(id);

                if (product == null)
                {
                    response.Message = "El producto no fue encontrado.";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Message = "Producto encontrado exitosamente.";
                response.Data = _mapper.Map<ProductDto>(product);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<ProductDto>>> CreateAsync(CreateProductEventCommand command)
        {
            Response<ProductDto> response = new Response<ProductDto>();

            try
            {
                CommandGenericResult<Product> commandResult = await _mediator.Send(command);

                if (!commandResult.IsSuccess)
                {
                    response.Message = commandResult.Message;
                    return BadRequest(response);
                }

                response.IsSuccess = true;
                response.Message = commandResult.Message;
                response.Data = _mapper.Map<ProductDto>(commandResult.Result);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<ProductDto>>> EditAsync(EditProductEventCommand command)
        {
            Response<ProductDto> response = new Response<ProductDto>();

            try
            {

                CommandGenericResult<Product> commandResult = await _mediator.Send(command);

                if (!commandResult.IsSuccess)
                {
                    response.Message = commandResult.Message;
                    return BadRequest(response);
                }

                response.IsSuccess = true;
                response.Message = commandResult.Message;
                response.Data = _mapper.Map<ProductDto>(commandResult.Result);
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
