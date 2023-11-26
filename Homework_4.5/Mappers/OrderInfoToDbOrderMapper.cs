using Homework_4._5.Controllers;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class OrderInfoToDbOrderMapper : IMapper<OrderInfo, DbOrder>
{
    public DbOrder Map(OrderInfo v)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Date = v.Date
        };
    }
}