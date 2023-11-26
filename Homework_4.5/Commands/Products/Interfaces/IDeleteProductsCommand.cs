using Core.Responses;

namespace Homework_4._5.Commands.Products.Interfaces;

public interface IDeleteProductCommand
{
    Task<bool> Execute(Guid id);
}