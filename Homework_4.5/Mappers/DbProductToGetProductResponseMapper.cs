using Core.Responses.Products;
using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class DbProductToGetProductResponseMapper : IMapper<DbProduct, GetProductResponse>
{
    public GetProductResponse Map(DbProduct v)
    {
        return new()
        {
            ProductName = v.ProductName,
            ProductPrice = v.Price
        };
    }
}