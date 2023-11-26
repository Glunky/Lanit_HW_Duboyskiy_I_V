using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers;

public class CreateCustomerCommand : ICreateCustomerCommand
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IMapper<CustomerInfo, DbCustomer> _mapper;
    private readonly IValidator<CustomerInfo> _validator;
    private readonly IHttpContextAccessor _accessor;

    public CreateCustomerCommand(
        IRepository<DbCustomer, CustomerInfo> repository,
        IMapper<CustomerInfo,DbCustomer> mapper,
        IValidator<CustomerInfo> validator,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _accessor = accessor;
    }
    
    public async Task<ResultResponse<Guid>> Execute(CustomerInfo request)
    {
        ValidationResult validationResult = _validator.Validate(request);
        
        if (!validationResult.IsValid)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            return new()
            {
                Errors = validationResult.Errors
            };
        }

        var dbCustomer = _mapper.Map(request);
        _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;

        await _repository.Create(dbCustomer);

        return new()
        {
            Body = dbCustomer.Id,
        };
    }
}