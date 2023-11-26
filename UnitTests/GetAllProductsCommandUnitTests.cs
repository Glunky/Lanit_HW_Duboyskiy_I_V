using Core.Responses.Products;
using Homework_4._5.Commands.Products;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests;

public class GetAllProductsCommandUnitTests
{
    private Mock<IRepository<DbProduct, ProductInfo>> _repository;
    private Mock<IMapper<DbProduct, GetProductResponse>> _mapper;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _repository = new();
        _repository
            .Setup(o => o.ReadAll())
            .ReturnsAsync(new []{new DbProduct(), new DbProduct()});

        _mapper = new();
        _mapper
            .Setup(o => o.Map(It.IsAny<DbProduct>()))
            .Returns(new GetProductResponse());
    }

    [Test]
    public async Task GetAllProductsCommandExecuteReturnTwoProducts()
    {
        var command = new GetAllProductsCommand(_repository.Object, _mapper.Object);

        var result = await command.Execute();
        
        Assert.AreEqual(result.GetProductResponses.Count(), 2);
    }
}