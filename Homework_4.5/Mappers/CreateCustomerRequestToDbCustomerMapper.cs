using Core.Requests.Customers;
using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class CreateCustomerRequestToDbCustomerMapper : IMapper<CreateCustomerRequest, DbCustomer>
{
    public DbCustomer Map(CreateCustomerRequest info)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            FirstName = info.FirstName,
            LastName = info.LastName
        };
    }
}
