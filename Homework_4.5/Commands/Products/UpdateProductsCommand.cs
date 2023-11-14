using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class UpdateProductCommand : IUpdateProductCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IValidator<ProductInfo> _validator;
    private readonly IHttpContextAccessor _accessor;

    public UpdateProductCommand(
        IRepository<DbProduct, ProductInfo> repository,
        IValidator<ProductInfo> validator,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _validator = validator;
        _accessor = accessor;
    }
    public async Task<ResultResponse<bool>> Execute(ProductInfo info)
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

        DbProduct product = await _repository.Read(info.Id.Value);

        await _repository.Update(product, info);

        return new()
        {
            Body = true
        };
    }
}