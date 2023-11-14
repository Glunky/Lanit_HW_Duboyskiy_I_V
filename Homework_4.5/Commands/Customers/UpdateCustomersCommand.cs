using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers;

public class UpdateCustomerCommand : IUpdateCustomerCommand
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IValidator<CustomerInfo> _validator;
    private readonly IHttpContextAccessor _accessor;

    public UpdateCustomerCommand(
        IRepository<DbCustomer, CustomerInfo> repository,
        IValidator<CustomerInfo> validator,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _validator = validator;
        _accessor = accessor;
    }
    public async Task<ResultResponse<bool>> Execute(CustomerInfo info)
    {
        ValidationResult validationResult = _validator.Validate(info);

        if (!validationResult.IsValid)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            return new()
            {
                Errors = validationResult.Errors
            };
        }

        DbCustomer customer = await _repository.Read(info.Id.Value);

        await _repository.Update(customer, info);

        return new()
        {
            Body = true
        };
    }
}