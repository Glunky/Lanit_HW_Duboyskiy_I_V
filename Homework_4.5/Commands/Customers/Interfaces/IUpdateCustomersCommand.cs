using Core.Requests;
using Core.Requests.Customers;
using Core.Responses;
using Homework_4._5.Requests;

namespace Homework_4._5.Commands.Customers.Interfaces;

public interface IUpdateCustomerCommand
{
    Task<bool> Execute(UpdateCustomerRequest info);
}