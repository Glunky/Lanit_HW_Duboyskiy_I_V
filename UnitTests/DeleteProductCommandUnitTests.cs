using Homework_4._5.Commands.Products;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class DeleteProductCommandUnitTests
{
    private Mock<IRepository<DbProduct, ProductInfo>> _repository;
    private Guid validGuid;
    private Guid invalidGuid;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        validGuid = Guid.NewGuid();
        invalidGuid = new();
        
        _repository = new();
        _repository
            .Setup(o => o.Read(validGuid))
            .ReturnsAsync(new DbProduct());

        _repository.Setup(o => o.Read(invalidGuid))
            .ReturnsAsync((DbProduct)null);
        
        _repository.Setup(o => o.Delete(It.IsAny<DbProduct>()))
            .Returns(Task.CompletedTask);
    }

    [Test]
    public async Task DeleteProductCommandExecuteReturnTrueIfGuidValid()
    {
        var command = new DeleteProductCommand(_repository.Object);

        var result = await command.Execute(validGuid);
        
        Assert.AreEqual(true, result);
    }
    
    [Test]
    public async Task DeleteProductCommandExecuteReturnFalseIfGuidInvalid()
    {
        var command = new DeleteProductCommand(_repository.Object);

        var result = await command.Execute(invalidGuid);
        
        Assert.AreEqual(false, result);
    }
}