using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface IDeleteOrderCommand
{
    Task<ResultResponse<bool>> Execute(Guid id);
}