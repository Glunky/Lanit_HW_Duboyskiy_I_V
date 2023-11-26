using Core.Requests.Customers;
using Core.Responses.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Consumers.Customers;

public class DeleteCustomerConsumer : IConsumer<DeleteCustomerRequest>
{
    private readonly IDeleteCustomerCommand _command;

    public DeleteCustomerConsumer([FromServices] IDeleteCustomerCommand command)
    {
        _command = command;
    }

    public async Task Consume(ConsumeContext<DeleteCustomerRequest> context)
    {
        DeleteCustomerResponse response = new()
        {
            IsDeleted = await _command.Execute(context.Message),
        };
        
       await context.RespondAsync(response);
    }
}