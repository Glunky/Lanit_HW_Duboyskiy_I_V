using Core.Requests;
using Core.Requests.Orders;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Validation.Orders;

public class CreateOrderValidator : IValidator<CreateOrderRequest>
{
    private readonly IRepository<DbProduct, ProductInfo> _productsRepository;
    private readonly IRepository<DbCustomer, CustomerInfo> _customersRepository;

    public CreateOrderValidator(
        IRepository<DbProduct, ProductInfo> productsRepository,
        IRepository<DbCustomer, CustomerInfo> customersRepository
        )
    {
        _productsRepository = productsRepository;
        _customersRepository = customersRepository;
    }
    
    public ValidationResult Validate(CreateOrderRequest request)
    {
        ValidationResult result = new();
        
        if (request == null)
        {
            result.Errors.Add("Bad request");
        }

        DbCustomer customer = _customersRepository.Read(request.CustomerId).Result;

        if (customer == null)
        {
            result.Errors.Add($"Cannot find customer with Id: {request.CustomerId}");
        }

        if (request.ProductsIds == null)
        {
            result.Errors.Add("ProductsIds is empty");
        }
        else
        {
            foreach (var productId in request.ProductsIds)
            {
                DbProduct product = _productsRepository.Read(productId).Result;

                if (product == null)
                {
                    result.Errors.Add($"Cannot find product with Id: {productId}");
                }
            }
        }
        

        if (result.Errors.Count == 0)
        {
            result.IsValid = true;
        }

        return result;
    }
}