using Core.Requests.Customers;
using Core.Responses.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Consumers.Customers;

public class UpdateCustomerConsumer : IConsumer<UpdateCustomerRequest>
{
    private readonly IUpdateCustomerCommand _command;

    public UpdateCustomerConsumer([FromServices] IUpdateCustomerCommand command)
    {
        _command = command;
    }

    public async Task Consume(ConsumeContext<UpdateCustomerRequest> context)
    {
       await context.RespondAsync(new UpdateCustomerResponse()
        {
            IsUpdated = await _command.Execute(context.Message)
        });
    }
}