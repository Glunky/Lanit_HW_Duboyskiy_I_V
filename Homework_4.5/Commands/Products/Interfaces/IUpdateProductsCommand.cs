using Core.Requests.Products;

namespace Homework_4._5.Commands.Products.Interfaces;

public interface IUpdateProductCommand
{
    Task<bool> Execute(UpdateProductRequest request);
}