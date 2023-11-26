using Core.Requests.Orders;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface IUpdateOrderCommand
{
    Task<bool> Execute(UpdateOrderRequest request);
}