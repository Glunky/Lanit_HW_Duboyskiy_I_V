using Core.Requests.Products;
using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class CreateProductRequestToDbProductMapper : IMapper<CreateProductRequest, DbProduct>
{
    public DbProduct Map(CreateProductRequest v)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            ProductName = v.ProductName,
            Price = v.ProductPrice,
        };
    }
}