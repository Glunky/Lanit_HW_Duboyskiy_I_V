using Core.Requests.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Orders;

public class GetAllOrdersConsumer : IConsumer<GetAllOrdersRequest>
{
    private readonly IGetAllOrdersCommand _command;

    public GetAllOrdersConsumer(IGetAllOrdersCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<GetAllOrdersRequest> context)
    {
        context.Respond(_command.Execute().Result);

        return Task.CompletedTask;
    }
}