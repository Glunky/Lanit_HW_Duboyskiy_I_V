using Core.Responses;
using Core.Responses.Orders;
using Homework_4._5.Controllers;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface IGetOrderCommand
{
    Task<GetOrderResponse> Execute(Guid id);
}