using Core.Requests.Orders;
using Core.Responses.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Consumers.Orders;

public class DeleteOrderConsumer : IConsumer<DeleteOrderRequest>
{
    private readonly IDeleteOrderCommand _command;

    public DeleteOrderConsumer([FromServices] IDeleteOrderCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<DeleteOrderRequest> context)
    {
        DeleteOrderResponse response = new()
        {
            IsDeleted = _command.Execute(context.Message.OrderId).Result,
        };
        
        context.Respond(response);

        return Task.CompletedTask;
    }
}