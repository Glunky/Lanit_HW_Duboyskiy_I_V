using Homework_4._5.Controllers;
using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface IGetOrderCommand
{
    Task<ResultResponse<OrderInfo>> Execute(Guid id);
}