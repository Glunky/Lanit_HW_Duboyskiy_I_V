using Homework_4._5.Controllers;
using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface IGetAllOrdersCommand
{
    Task<ResultResponse<IEnumerable<OrderInfo>>> Execute();
}