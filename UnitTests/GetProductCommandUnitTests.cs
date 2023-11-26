using Core.Responses.Products;
using Homework_4._5.Commands.Products;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class GetProductCommandUnitTests
{
    private Mock<IRepository<DbProduct, ProductInfo>> _repository;
    private Mock<IMapper<DbProduct, GetProductResponse>> _mapper;
    private DbProduct testDbProduct;
    private GetProductResponse testGetProductResponce;
    private Guid validGuid;
    private Guid invalidGuid;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        validGuid = Guid.NewGuid();
        invalidGuid = new();
        
        testDbProduct = new()
        {
            ProductName = "Potato",
            Price = 150
        };

        testGetProductResponce = new()
        {
            ProductName = testDbProduct.ProductName,
            ProductPrice = testDbProduct.Price
        };
        
        _repository = new();
        _repository
            .Setup(o => o.Read(validGuid))
            .ReturnsAsync(testDbProduct);

        _repository.Setup(o => o.Read(invalidGuid))
            .ReturnsAsync((DbProduct)null);

        _mapper = new();
        _mapper
            .Setup(o => o.Map(testDbProduct))
            .Returns(testGetProductResponce);
    }

    [Test]
    public async Task GetProductCommandExecuteReturnResponseIfGuidValid()
    {
        var command = new GetProductCommand(_repository.Object, _mapper.Object);

        var result = await command.Execute(validGuid);
        
        Assert.AreEqual(testDbProduct.ProductName, result.ProductName);
        Assert.AreEqual(testDbProduct.Price, result.ProductPrice);
    }
    
    [Test]
    public async Task GetProductCommandExecuteReturnNullIfRequestNull()
    {
        var command = new GetProductCommand(_repository.Object, _mapper.Object);

        var result = await command.Execute(invalidGuid);
        
        Assert.Null(result);
    }
}