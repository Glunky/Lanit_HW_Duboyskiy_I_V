using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICreateProductCommand _createProductCommand;
    private readonly IGetProductCommand _getProductCommand;
    private readonly IGetAllProductsCommand _getAllProductsCommand;
    private readonly IUpdateProductCommand _updateProductsCommand;
    private readonly IDeleteProductCommand _deleteProductsCommand;

    public ProductsController(
        ICreateProductCommand createProductCommand,
        IGetProductCommand getProductCommand,
        IGetAllProductsCommand getAllProductsCommand,
        IUpdateProductCommand updateProductsCommand,
        IDeleteProductCommand deleteProductsCommand)
    {
        _createProductCommand = createProductCommand;
        _getProductCommand = getProductCommand;
        _getAllProductsCommand = getAllProductsCommand;
        _updateProductsCommand = updateProductsCommand;
        _deleteProductsCommand = deleteProductsCommand;
    }

    [HttpPost]
    public async Task<ResultResponse<Guid>> Create([FromBody] ProductInfo request)
    {
        return await _createProductCommand.Execute(request);
    }

    [HttpGet("{id}")]
    public async Task<ResultResponse<ProductInfo>> Get([FromRoute] Guid id)
    {
        return await _getProductCommand.Execute(id);
    }

    [HttpGet]
    public async Task<ResultResponse<IEnumerable<ProductInfo>>> Get()
    {
        return await _getAllProductsCommand.Execute();
    }

    [HttpPut]
    public async Task<ResultResponse<bool>> Update([FromBody] ProductInfo request)
    {
        return await _updateProductsCommand.Execute(request);
    }

    [HttpDelete("{id}")]
    public async Task<ResultResponse<bool>> Delete([FromRoute] Guid id)
    {
        return await _deleteProductsCommand.Execute(id);
    }
}