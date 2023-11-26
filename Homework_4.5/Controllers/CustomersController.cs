using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICreateCustomerCommand _createCustomerCommand;
    private readonly IGetCustomerCommand _getCustomerCommand;
    private readonly IGetAllCustomersCommand _getAllCustomersCommand;
    private readonly IUpdateCustomerCommand _updateCustomersCommand;
    private readonly IDeleteCustomerCommand _deleteCustomersCommand;

    public CustomersController(
        ICreateCustomerCommand createCustomerCommand,
        IGetCustomerCommand getCustomerCommand,
        IGetAllCustomersCommand getAllCustomersCommand,
        IUpdateCustomerCommand updateCustomersCommand,
        IDeleteCustomerCommand deleteCustomersCommand)
    {
        _createCustomerCommand = createCustomerCommand;
        _getCustomerCommand = getCustomerCommand;
        _getAllCustomersCommand = getAllCustomersCommand;
        _updateCustomersCommand = updateCustomersCommand;
        _deleteCustomersCommand = deleteCustomersCommand;
    }

    [HttpPost]
    public async Task<ResultResponse<Guid>> Create([FromBody] CustomerInfo request)
    {
        return await _createCustomerCommand.Execute(request);
    }

    [HttpGet("{id}")]
    public async Task<ResultResponse<CustomerInfo>> Get([FromRoute] Guid id)
    {
        return await _getCustomerCommand.Execute(id);
    }
    
    [HttpGet]
    public async Task<ResultResponse<IEnumerable<CustomerInfo>>> Get()
    {
      return await _getAllCustomersCommand.Execute();  
    }

    [HttpPut]
    public async Task<ResultResponse<bool>> Update([FromBody] CustomerInfo request)
    {
        return await _updateCustomersCommand.Execute(request);
    }
    
    [HttpDelete("{id}")]
    public async Task<ResultResponse<bool>> Delete([FromRoute] Guid id)
    {
        return await _deleteCustomersCommand.Execute(id);
    }
}