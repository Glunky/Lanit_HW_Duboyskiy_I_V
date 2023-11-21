using Core.Requests.Products;
using Core.Responses;
using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class CreateProductCommand : ICreateProductCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IMapper<CreateProductRequest, DbProduct> _mapper;
    private readonly IValidator<CreateProductRequest> _validator;

    public CreateProductCommand(
        IRepository<DbProduct, ProductInfo> repository,
        IMapper<CreateProductRequest, DbProduct> mapper,
        IValidator<CreateProductRequest> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<Guid?> Execute(CreateProductRequest request)
    {
        ValidationResult validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return null;
        }

        var dbProduct = _mapper.Map(request);

        await _repository.Create(dbProduct);

        return dbProduct.Id;
    }
}