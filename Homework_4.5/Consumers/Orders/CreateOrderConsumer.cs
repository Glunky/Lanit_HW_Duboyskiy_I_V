using Core.Requests.Orders;
using Core.Responses.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Orders;

public class CreateOrderConsumer : IConsumer<CreateOrderRequest>
{
    private readonly ICreateOrderCommand _command;

    public CreateOrderConsumer(ICreateOrderCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<CreateOrderRequest> context)
    {
        Guid? OrderId = _command.Execute(context.Message).Result;

        CreateOrderResponse response = new()
        {
            OrderId = OrderId,
        };
        
        context.Respond(response);

        return Task.CompletedTask;
    }
}