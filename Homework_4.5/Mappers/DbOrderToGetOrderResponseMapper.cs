using Core.Responses.Orders;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class DbOrderToGetOrderResponseMapper : IMapper<DbOrder, GetOrderResponse>
{
    public GetOrderResponse Map(DbOrder v)
    {
        return new()
        {
            CustomerId = v.CustomerId,
            Date = v.Date,
            ProductsIds = v.Products.Select(p => p.Id).ToList()
        };
    }
}