using Core.Requests.Customers;
using Core.Responses;
using Core.Responses.Customers;
using Homework_5.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomersController
{
    private readonly IHttpContextAccessor _accessor;

    public CustomersController([FromServices] IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    [HttpPost]
    public async Task<ResultResponse<Guid>> Create(
        [FromServices] IMessagePublisher<CreateCustomerRequest, CreateCustomerResponse> messagePublisher,
        [FromBody] CreateCustomerRequest request)
    {
        var result = await messagePublisher.SendMessageAsync(request);

        if (result.CustomerId == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            return new()
            {
                Errors = new() { "Creation of customer failed" }
            };
        }
        
        _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        
        return new()
        {
            Body = result.CustomerId.Value
        };
    }
    
    [HttpGet("{id}")]
    public async Task<ResultResponse<GetCustomerResponse>> Get(
        [FromServices] IMessagePublisher<GetCustomerRequest, GetCustomerResponse> messagePublisher,
        [FromRoute] Guid id
        )
    {
        GetCustomerResponse result = await messagePublisher.SendMessageAsync(new GetCustomerRequest { CustomerId = id });

        if (result == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot get customer with Id {id}" }
            };
        }
        
        return new()
        {
            Body = result
        };
    }
    
    [HttpGet]
    public async Task<ResultResponse<GetAllCustomersResponse>> Get(
        [FromServices] IMessagePublisher<GetAllCustomersRequest, GetAllCustomersResponse> messagePublisher)
    {
        GetAllCustomersResponse result = await messagePublisher.SendMessageAsync(new GetAllCustomersRequest());

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
        [FromServices] IMessagePublisher<UpdateCustomerRequest, UpdateCustomerResponse> messagePublisher,
        [FromBody] UpdateCustomerRequest request)
    {
        var result = await messagePublisher.SendMessageAsync(request);

        if (result == null || !result.IsUpdated)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot update customer with Id {request.Id}" }
            };
        }
        
        return new()
        {
            Body = result.IsUpdated
        };
    }
    
    [HttpDelete("{id}")]
    public async Task<ResultResponse<bool>> Delete(
        [FromServices] IMessagePublisher<DeleteCustomerRequest, DeleteCustomerResponse> messagePublisher,
        [FromRoute] Guid id)
    {
        var result = await messagePublisher.SendMessageAsync(new DeleteCustomerRequest {CustomerId = id});

        if (result == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot delete customer with Id {id}" }
            };
        }
        
        return new()
        {
            Body = result.IsDeleted
        };
    }
}
