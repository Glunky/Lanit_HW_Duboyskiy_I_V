using Core.Requests.Customers;

namespace Homework_4._5.Commands.Customers.Interfaces;

public interface ICreateCustomerCommand
{
    Task<Guid?> Execute(CreateCustomerRequest request);
}