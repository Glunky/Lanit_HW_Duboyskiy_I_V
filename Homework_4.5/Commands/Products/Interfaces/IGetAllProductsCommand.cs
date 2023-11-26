using Core.Responses;
using Core.Responses.Products;
using Homework_4._5.Requests;

namespace Homework_4._5.Commands.Products.Interfaces;

public interface IGetAllProductsCommand
{
    Task<GetAllProductsResponse> Execute();
}