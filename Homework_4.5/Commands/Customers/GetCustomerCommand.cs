using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers;

public class GetCustomerCommand : IGetCustomerCommand
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IMapper<DbCustomer, CustomerInfo> _mapper;
    private readonly IHttpContextAccessor _accessor;

    public GetCustomerCommand(
        IRepository<DbCustomer, CustomerInfo> repository, 
        IMapper<DbCustomer, CustomerInfo> mapper,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _mapper = mapper;
        _accessor = accessor;
    }
    
    public async Task<ResultResponse<CustomerInfo>> Execute(Guid id)
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

        return new()
        {
            Body = _mapper.Map(customer)
        };
    }
}