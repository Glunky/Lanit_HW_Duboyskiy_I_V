using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class ProductInfoToDbProductMapper : IMapper<ProductInfo, DbProduct>
{
    public DbProduct Map(ProductInfo v)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            ProductName = v.ProductName,
            Price = v.Price,
        };
    }
}