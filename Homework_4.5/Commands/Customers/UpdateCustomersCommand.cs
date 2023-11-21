using Core.Requests;
using Core.Requests.Customers;
using Core.Responses;
using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Requests;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers;

public class UpdateCustomerCommand : IUpdateCustomerCommand
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IValidator<UpdateCustomerRequest> _validator;

    public UpdateCustomerCommand(
        IRepository<DbCustomer, CustomerInfo> repository,
        IValidator<UpdateCustomerRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<bool> Execute(UpdateCustomerRequest info)
    {
        ValidationResult validationResult = _validator.Validate(info);

        if (!validationResult.IsValid)
        {
            return false;
        }

        DbCustomer customer = await _repository.Read(info.Id);

        await _repository.Update(customer, new CustomerInfo()
        {
            FirstName = info.FirstName,
            LastName = info.LastName
        });

        return true;
    }
}