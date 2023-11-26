using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class DbCustomerToCustomerInfoMapper : IMapper<DbCustomer, CustomerInfo>
{
    public CustomerInfo Map(DbCustomer v)
    {
        return new()
        {
            Id = v.Id,
            FirstName = v.FirstName,
            LastName = v.LastName
        };
    }
}