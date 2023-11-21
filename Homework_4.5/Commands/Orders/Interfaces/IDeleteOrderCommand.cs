using Core.Responses;

namespace Homework_4._5.Commands.Orders.Interfaces;

public interface IDeleteOrderCommand
{
    Task<bool> Execute(Guid id);
}