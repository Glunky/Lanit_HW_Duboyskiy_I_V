using Core.Requests;
using Core.Requests.Customers;
using Core.Responses;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Commands.Customers.Interfaces;

public class DeleteCustomerCommand : IDeleteCustomerCommand
{
    private IRepository<DbCustomer, CustomerInfo> _repository;

    public DeleteCustomerCommand([FromServices] IRepository<DbCustomer, CustomerInfo> repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> Execute(DeleteCustomerRequest request)
    {
        DbCustomer customer = await _repository.Read(request.CustomerId);

        if (customer == null)
        {
            return false;
        }

        await _repository.Delete(customer);

        return true;
    }
}