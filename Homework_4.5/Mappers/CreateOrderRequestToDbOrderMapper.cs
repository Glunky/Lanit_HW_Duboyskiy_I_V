using Core.Requests.Orders;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class CreateOrderRequestToDbOrderMapper : IMapper<CreateOrderRequest, DbOrder>
{
    public DbOrder Map(CreateOrderRequest request)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Date = request.Date
        };
    }
}