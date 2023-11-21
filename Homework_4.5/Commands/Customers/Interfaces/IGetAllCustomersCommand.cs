using Core.Requests.Customers;
using Core.Responses;
using Core.Responses.Customers;
using Homework_4._5.Requests;

namespace Homework_4._5.Commands.Customers.Interfaces;

public interface IGetAllCustomersCommand
{
    Task<GetAllCustomersResponse> Execute();
}