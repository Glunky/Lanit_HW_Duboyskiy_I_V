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

    public async Task Consume(ConsumeContext<GetOrderRequest> context)
    {
       await context.RespondAsync(await _command.Execute(context.Message.OrderId));
    }
}