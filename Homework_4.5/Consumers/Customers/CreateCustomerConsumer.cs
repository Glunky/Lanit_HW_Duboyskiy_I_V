using Core.Requests.Customers;
using Core.Responses.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Customers;

public class CreateCustomerConsumer : IConsumer<CreateCustomerRequest>
{
    private readonly ICreateCustomerCommand _command;

    public CreateCustomerConsumer(ICreateCustomerCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<CreateCustomerRequest> context)
    {
        Guid? customerId = _command.Execute(context.Message).Result;

        CreateCustomerResponse response = new()
        {
            CustomerId = customerId,
        };
        
        context.Respond(response);

        return Task.CompletedTask;
    }
}