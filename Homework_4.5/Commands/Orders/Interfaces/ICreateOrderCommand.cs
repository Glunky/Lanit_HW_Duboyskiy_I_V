using Homework_4._5.Controllers;
using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface ICreateOrderCommand
{
    Task<ResultResponse<Guid>> Execute(OrderInfo request);
}