using Core.Requests.Products;
using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Requests;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class UpdateProductCommand : IUpdateProductCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IValidator<UpdateProductRequest> _validator;

    public UpdateProductCommand(
        IRepository<DbProduct, ProductInfo> repository,
        IValidator<UpdateProductRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<bool> Execute(UpdateProductRequest info)
    {
        ValidationResult validationResult = _validator.Validate(info);

        if (!validationResult.IsValid)
        {
            return false;
        }

        DbProduct product = await _repository.Read(info.ProductId);

        await _repository.Update(product, new ProductInfo()
        {
            ProductName = info.ProductName,
            Price = info.ProductPrice
        });

        return true;
    }
}