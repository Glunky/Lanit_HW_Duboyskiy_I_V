using Core.Requests.Customers;
using Core.Responses.Customers;
using Homework_4._5.Requests;
using Homework_4.DbModels;

namespace Homework_4._5.Mappers;

public class DbCustomerToGetCustomerResponseMapper : IMapper<DbCustomer, GetCustomerResponse>
{
    public GetCustomerResponse Map(DbCustomer v)
    {
        return new()
        {
            FirstName = v.FirstName,
            LastName = v.LastName
        };
    }
}
