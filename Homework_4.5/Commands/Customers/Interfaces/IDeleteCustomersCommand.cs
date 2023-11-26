using Core.Requests.Customers;

namespace Homework_4._5.Commands.Customers.Interfaces;

public interface IDeleteCustomerCommand
{
    Task<bool> Execute(DeleteCustomerRequest id);
}