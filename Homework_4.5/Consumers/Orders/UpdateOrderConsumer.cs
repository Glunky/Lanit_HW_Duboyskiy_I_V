using Core.Requests.Orders;
using Core.Responses.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Consumers.Orders;

public class UpdateOrderConsumer : IConsumer<UpdateOrderRequest>
{
    private readonly IUpdateOrderCommand _command;

    public UpdateOrderConsumer([FromServices] IUpdateOrderCommand command)
    {
        _command = command;
    }

    public async Task Consume(ConsumeContext<UpdateOrderRequest> context)
    {
       await context.RespondAsync(new UpdateOrderResponse()
        {
            IsUpdated = await _command.Execute(context.Message)
        });
    }
}