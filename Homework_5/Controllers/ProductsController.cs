using Core.Requests.Products;
using Core.Responses;
using Core.Responses.Products;
using Homework_5.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController
{
    private readonly IHttpContextAccessor _accessor;

    public ProductsController([FromServices] IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    [HttpPost]
    public async Task<ResultResponse<Guid>> Create(
        [FromServices] IMessagePublisher<CreateProductRequest, CreateProductResponse> messagePublisher,
        [FromBody] CreateProductRequest request)
    {
        var result = await messagePublisher.SendMessageAsync(request);

        if (result.ProductId == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            return new()
            {
                Errors = new() { "Creation of Products failed" }
            };
        }
        
        _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        
        return new()
        {
            Body = result.ProductId.Value
        };
    }
    
    [HttpGet("{id}")]
    public async Task<ResultResponse<GetProductResponse>> Get(
        [FromServices] IMessagePublisher<GetProductRequest, GetProductResponse> messagePublisher,
        [FromRoute] Guid id
    )
    {
        GetProductResponse result = await messagePublisher.SendMessageAsync(new GetProductRequest { ProductId = id });

        if (result == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot get Product with Id {id}" }
            };
        }
        
        return new()
        {
            Body = result
        };
    }
    
    [HttpGet]
    public async Task<ResultResponse<GetAllProductsResponse>> Get(
        [FromServices] IMessagePublisher<GetAllProductsRequest, GetAllProductsResponse> messagePublisher)
    {
        GetAllProductsResponse result = await messagePublisher.SendMessageAsync(new GetAllProductsRequest());

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
        [FromServices] IMessagePublisher<UpdateProductRequest, UpdateProductResponse> messagePublisher,
        [FromBody] UpdateProductRequest request)
    {
        var result = await messagePublisher.SendMessageAsync(request);

        if (result == null || !result.IsUpdated)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot update Product with Id {request.ProductId}" }
            };
        }
        
        return new()
        {
            Body = result.IsUpdated
        };
    }
    
    [HttpDelete("{id}")]
    public async Task<ResultResponse<bool>> Delete(
        [FromServices] IMessagePublisher<DeleteProductRequest, DeleteProductResponse> messagePublisher,
        [FromRoute] Guid id)
    {
        var result = await messagePublisher.SendMessageAsync(new DeleteProductRequest {ProductId = id});

        if (result == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new() { $"Cannot delete Product with Id {id}" }
            };
        }
        
        return new()
        {
            Body = result.IsDeleted
        };
    }
}