using Core.Requests.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Customers;

public class GetAllCustomersConsumer : IConsumer<GetAllCustomersRequest>
{
    private readonly IGetAllCustomersCommand _command;

    public GetAllCustomersConsumer(IGetAllCustomersCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<GetAllCustomersRequest> context)
    {
        context.Respond(_command.Execute().Result);

        return Task.CompletedTask;
    }
}