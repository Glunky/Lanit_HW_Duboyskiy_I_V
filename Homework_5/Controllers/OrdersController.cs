using Core.Requests.Orders;
using Core.Responses;
using Core.Responses.Orders;
using Homework_5.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController
{
    private readonly IHttpContextAccessor _accessor;

    public OrdersController([FromServices] IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    [HttpPost]
    public ResultResponse<Guid> Create(
        [FromServices] IMessagePublisher<CreateOrderRequest, CreateOrderResponse> messagePublisher,
        [FromBody] CreateOrderRequest request)
    {
        var result = messagePublisher.SendMessage(request);

        if (result.OrderId == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            return new()
            {
                Errors = new() { "Creation of Order failed" }
            };
        }
        
        _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        
        return new()
        {
            Body = result.OrderId.Value
        };
    }
    
    [HttpGet("{id}")]
    public async Task<ResultResponse<GetOrderResponse>> Get(
        [FromServices] IMessagePublisher<GetOrderRequest, GetOrderResponse> messagePublisher,
        [FromRoute] Guid id
        )
    {
        GetOrderResponse result = messagePublisher.SendMessage(new GetOrderRequest { OrderId = id });

        if (result == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot get Order with Id {id}" }
            };
        }
        
        return new()
        {
            Body = result
        };
    }
    
    [HttpGet]
    public async Task<ResultResponse<GetAllOrdersResponse>> Get(
        [FromServices] IMessagePublisher<GetAllOrdersRequest, GetAllOrdersResponse> messagePublisher)
    {
        GetAllOrdersResponse result = messagePublisher.SendMessage(new GetAllOrdersRequest());

        if (result == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return new()
            {
                Errors = new() { "Something wrong" }
            };
        }
        
        return new()
        {
            Body = result
        };
    }
    
    [HttpPut]
    public async Task<ResultResponse<bool>> Update(
        [FromServices] IMessagePublisher<UpdateOrderRequest, UpdateOrderResponse> messagePublisher,
        [FromBody] UpdateOrderRequest request)
    {
        var result = messagePublisher.SendMessage(request);

        if (result == null || !result.IsUpdated)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot update Order with Id {request.OrderId}" }
            };
        }
        
        return new()
        {
            Body = result.IsUpdated
        };
    }
    
    [HttpDelete("{id}")]
    public async Task<ResultResponse<bool>> Delete(
        [FromServices] IMessagePublisher<DeleteOrderRequest, DeleteOrderResponse> messagePublisher,
        [FromRoute] Guid id)
    {
        var result = messagePublisher.SendMessage(new DeleteOrderRequest { OrderId = id});

        if (result == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot delete Order with Id {id}" }
            };
        }
        
        return new()
        {
            Body = result.IsDeleted
        };
    }
}
