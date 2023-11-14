using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class CustomerInfoToDbCustomerMapper : IMapper<CustomerInfo, DbCustomer>
{
    public DbCustomer Map(CustomerInfo info)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            FirstName = info.FirstName,
            LastName = info.LastName
        };
    }
}