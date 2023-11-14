using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class DbProductToProductInfoMapper : IMapper<DbProduct, ProductInfo>
{
    public ProductInfo Map(DbProduct v)
    {
        return new()
        {
            Id = v.Id,
            ProductName = v.ProductName,
            Price = v.Price
        };
    }
}