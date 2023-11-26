using Core.Requests.Products;
using Homework_4._5.Commands.Products;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class CreateProductCommandUnitTests
{
    private Mock<IRepository<DbProduct, ProductInfo>> _repository;
    private Mock<IMapper<CreateProductRequest, DbProduct>> _mapper;
    private Mock<IValidator<CreateProductRequest>> _validator;
    private DbProduct testDbProduct;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        testDbProduct = new DbProduct()
        {
            Id = Guid.NewGuid(),
            ProductName = It.IsAny<string>(),
            Price = It.IsAny<decimal>()
        };

        _repository = new ();
        _repository
            .Setup(o => o.Create(It.IsAny<DbProduct>()))
            .Returns(Task.CompletedTask);

        _mapper = new ();
        _mapper
            .Setup(o => o.Map(It.IsAny<CreateProductRequest>()))
            .Returns(testDbProduct);

        _validator = new ();
        _validator
            .Setup(o => o.Validate(It.IsAny<CreateProductRequest>()))
            .Returns(new ValidationResult()
            {
                IsValid = true,
                Errors = new()
            });

        _validator
            .Setup(o => o.Validate(null))
            .Returns(new ValidationResult()
            {
                IsValid = false,
                Errors = new List<string> { "Bad Request" }
            });
    }

    [Test]
    public async Task CreateProductCommandExecuteReturnProductGuidIfRequestOk()
    {
        var command = new CreateProductCommand(_repository.Object, _mapper.Object, _validator.Object);

        var result = await command.Execute(new CreateProductRequest()
        {
            ProductName = "Potato",
            ProductPrice = 150
        });
        
        Assert.AreEqual(testDbProduct.Id, result);
    }
    
    [Test]
    public async Task CreateProductCommandExecuteReturnProductGuidIfRequestNull()
    {
        var command = new CreateProductCommand(_repository.Object, _mapper.Object, _validator.Object);

        var result = await command.Execute(null);
        
        Assert.AreEqual(null, result);
    }
}