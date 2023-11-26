using Core.Requests.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Orders;

public class GetOrderConsumer : IConsumer<GetOrderRequest>
{
    private readonly IGetOrderCommand _command;

    public GetOrderConsumer(IGetOrderCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<GetOrderRequest> context)
    {
        context.Respond(_command.Execute(context.Message.OrderId).Result);

        return Task.CompletedTask;
    }
}