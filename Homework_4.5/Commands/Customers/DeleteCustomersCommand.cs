using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers.Interfaces;

public class DeleteCustomerCommand : IDeleteCustomerCommand
{
    private IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IHttpContextAccessor _accessor;

    public DeleteCustomerCommand(
        IRepository<DbCustomer, CustomerInfo> repository, IHttpContextAccessor accessor)
    {
        _repository = repository;
        _accessor = accessor;
    }
    
    public async Task<ResultResponse<bool>> Execute(Guid id)
    {
        DbCustomer customer = await _repository.Read(id);

        if (customer == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            return new()
            {
                Errors = new()
                {
                    $"Cannot find customer with Id {id}"
                }
            };
        }

        await _repository.Delete(customer);

        return new()
        {
            Body = true
        };
    }
}