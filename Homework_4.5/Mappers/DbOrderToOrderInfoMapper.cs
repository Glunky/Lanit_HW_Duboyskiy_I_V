using Homework_4._5.Controllers;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class DbOrderToOrderInfoMapper : IMapper<DbOrder, OrderInfo>
{
    public OrderInfo Map(DbOrder v)
    {
        return new()
        {
            CustomerId = v.CustomerId,
            Date = v.Date,
            ProductsIds = v.Products.Select(p => p.Id).ToList()
        };
    }
}