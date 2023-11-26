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

    public async Task Consume(ConsumeContext<GetCustomerRequest> context)
    {
       await context.RespondAsync(await _command.Execute(context.Message));
    }
}
