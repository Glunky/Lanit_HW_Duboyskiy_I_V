using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class CreateProductCommand : ICreateProductCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IMapper<ProductInfo, DbProduct> _mapper;
    private readonly IValidator<ProductInfo> _validator;
    private readonly IHttpContextAccessor _accessor;

    public CreateProductCommand(
        IRepository<DbProduct, ProductInfo> repository,
        IMapper<ProductInfo, DbProduct> mapper,
        IValidator<ProductInfo> validator,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _accessor = accessor;
    }
    
    public async Task<ResultResponse<Guid>> Execute(ProductInfo request)
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

        var dbProduct = _mapper.Map(request);
        _accessor.HttpContext.Response.StatusCode = StatusCodes.Status201Created;

        await _repository.Create(dbProduct);

        return new()
        {
            Body = dbProduct.Id,
        };
    }
}