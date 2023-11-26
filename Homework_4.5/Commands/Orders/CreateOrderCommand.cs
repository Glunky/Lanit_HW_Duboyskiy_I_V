using Core.Requests;
using Core.Requests.Customers;
using Core.Requests.Orders;
using Core.Responses;
using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class CreateOrderCommand : ICreateOrderCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;
    private readonly IRepository<DbCustomer, CustomerInfo> _customerRepository;
    private readonly IRepository<DbProduct, ProductInfo> _productRepository;
    private readonly IMapper<CreateOrderRequest, DbOrder> _mapper;
    private readonly IValidator<CreateOrderRequest> _validator;

    public CreateOrderCommand(
        IRepository<DbOrder, OrderInfo> repository,
        IRepository<DbCustomer, CustomerInfo> customerRepository, 
        IRepository<DbProduct, ProductInfo> productRepository,
        IMapper<CreateOrderRequest, DbOrder> mapper,
        IValidator<CreateOrderRequest> validator)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<Guid?> Execute(CreateOrderRequest request)
    {
        ValidationResult validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return null;
        }

        DbOrder dbOrder = _mapper.Map(request);
        IEnumerable<Task<DbProduct>> products = request.ProductsIds.Select(async id => await _productRepository.Read(id));

        dbOrder.Customer = await _customerRepository.Read(request.CustomerId);
        dbOrder.Products = products.Select(p=>p.Result).ToList();

        await _repository.Create(dbOrder);
        await _repository.SaveChangesAsync();

        return dbOrder.Id;
    }
}