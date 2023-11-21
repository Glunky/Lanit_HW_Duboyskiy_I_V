using Core.Requests.Orders;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface ICreateOrderCommand
{
    Task<Guid?> Execute(CreateOrderRequest request);
}