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

    public async Task Consume(ConsumeContext<GetAllCustomersRequest> context)
    {
       await context.RespondAsync(await _command.Execute());
    }
}