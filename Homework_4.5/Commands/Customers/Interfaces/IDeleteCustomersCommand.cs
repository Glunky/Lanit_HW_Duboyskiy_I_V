using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Customers.Interfaces;

public interface IDeleteCustomerCommand
{
    Task<ResultResponse<bool>> Execute(Guid id);
}