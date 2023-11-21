using Core.Responses.Orders;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface IGetAllOrdersCommand
{
    Task<GetAllOrdersResponse> Execute();
}