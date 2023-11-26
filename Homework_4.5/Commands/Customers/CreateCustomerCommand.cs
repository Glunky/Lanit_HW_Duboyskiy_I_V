using Core.Requests;
using Core.Requests.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers;

public class CreateCustomerCommand : ICreateCustomerCommand
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IMapper<CreateCustomerRequest, DbCustomer> _mapper;
    private readonly IValidator<CreateCustomerRequest> _validator;

    public CreateCustomerCommand(
        IRepository<DbCustomer, CustomerInfo> repository,
        IMapper<CreateCustomerRequest,DbCustomer> mapper,
        IValidator<CreateCustomerRequest> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<Guid?> Execute(CreateCustomerRequest request)
    {
        ValidationResult validationResult = _validator.Validate(request);
        
        if (!validationResult.IsValid)
        {
            return default;
        }

        var dbCustomer = _mapper.Map(request);

        await _repository.Create(dbCustomer);

        return dbCustomer.Id;
    }
}