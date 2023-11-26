using Homework_4._5.Requests;
using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Customers.Interfaces;

public interface IUpdateCustomerCommand
{
    Task<ResultResponse<bool>> Execute(CustomerInfo info);
}