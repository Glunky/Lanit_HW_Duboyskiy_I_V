using Homework_4._5.Requests;
using Homework_4._5.Responces;

namespace Homework_4._5.Commands.Products.Interfaces;

public interface IUpdateProductCommand
{
    Task<ResultResponse<bool>> Execute(ProductInfo request);
}