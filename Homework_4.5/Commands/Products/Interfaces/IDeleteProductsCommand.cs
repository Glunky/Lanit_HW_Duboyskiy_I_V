using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Products.Interfaces;

public interface IDeleteProductCommand
{
    Task<ResultResponse<bool>> Execute(Guid id);
}