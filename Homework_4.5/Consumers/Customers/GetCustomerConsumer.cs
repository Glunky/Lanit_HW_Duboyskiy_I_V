using Core.Requests.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Customers;

public class GetCustomerConsumer : IConsumer<GetCustomerRequest>
{
    private readonly IGetCustomerCommand _command;

    public GetCustomerConsumer(IGetCustomerCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<GetCustomerRequest> context)
    {
        context.Respond(_command.Execute(context.Message).Result);

        return Task.CompletedTask;
    }
}
