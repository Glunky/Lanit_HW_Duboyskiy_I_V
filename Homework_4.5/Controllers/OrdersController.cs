using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Responces;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderCommand _createOrderCommand;
    private readonly IGetOrderCommand _getOrderCommand;
    private readonly IGetAllOrdersCommand _getAllOrdersCommand;
    private readonly IUpdateOrderCommand _updateOrdersCommand;
    private readonly IDeleteOrderCommand _deleteOrdersCommand;

    public OrdersController(
        ICreateOrderCommand createOrderCommand,
        IGetOrderCommand getOrderCommand,
        IGetAllOrdersCommand getAllOrdersCommand,
        IUpdateOrderCommand updateOrdersCommand,
        IDeleteOrderCommand deleteOrdersCommand)
    {
        _createOrderCommand = createOrderCommand;
        _getOrderCommand = getOrderCommand;
        _getAllOrdersCommand = getAllOrdersCommand;
        _updateOrdersCommand = updateOrdersCommand;
        _deleteOrdersCommand = deleteOrdersCommand;
    }

    [HttpPost]
    public async Task<ResultResponse<Guid>> Create([FromBody] OrderInfo request)
    {
        return await _createOrderCommand.Execute(request);
    }

    [HttpGet("{id}")]
    public async Task<ResultResponse<OrderInfo>> Get([FromRoute] Guid id)
    {
        return await _getOrderCommand.Execute(id);
    }
    
    [HttpGet]
    public async Task<ResultResponse<IEnumerable<OrderInfo>>> Get()
    {
        return await _getAllOrdersCommand.Execute();  
    }

    [HttpPut]
    public async Task<ResultResponse<bool>> Update([FromBody] OrderInfo request)
    {
        return await _updateOrdersCommand.Execute(request);
    }
    
    [HttpDelete("{id}")]
    public async Task<ResultResponse<bool>> Delete([FromRoute] Guid id)
    {
        return await _deleteOrdersCommand.Execute(id);
    }
}