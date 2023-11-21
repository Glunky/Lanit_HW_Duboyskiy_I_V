using Core.Requests.Customers;
using Core.Responses.Customers;

namespace Homework_4._5.Commands.Customers.Interfaces;

public interface IGetCustomerCommand
{
    Task<GetCustomerResponse> Execute(GetCustomerRequest request);
}
