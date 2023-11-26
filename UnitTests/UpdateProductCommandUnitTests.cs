using Core.Requests.Products;
using Homework_4._5.Commands.Products;
using Homework_4._5.Requests;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class UpdateProductCommandUnitTests
{
    private Mock<IRepository<DbProduct, ProductInfo>> _repository;
    private Mock<IValidator<UpdateProductRequest>> _validator;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _repository = new ();
        _repository
            .Setup(o => o.Read(It.IsAny<Guid>()))
            .ReturnsAsync(new DbProduct());

        _repository
            .Setup(o => o.Update(It.IsAny<DbProduct>(), It.IsAny<ProductInfo>()))
            .Returns(Task.CompletedTask);

        _validator = new ();
        _validator
            .Setup(o => o.Validate(It.IsAny<UpdateProductRequest>()))
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
    public async Task UpdateProductCommandExecuteReturnTrueIfRequestOk()
    {
        var command = new UpdateProductCommand(_repository.Object, _validator.Object);

        var result = await command.Execute(new UpdateProductRequest
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Potato",
            ProductPrice = 150
        });
        
        Assert.AreEqual(true, result);
    }
    
    [Test]
    public async Task UpdateProductCommandExecuteReturnFalseIfRequestNull()
    {
        var command = new UpdateProductCommand(_repository.Object, _validator.Object);

        var result = await command.Execute(null);
        
        Assert.AreEqual(false, result);
    }
}