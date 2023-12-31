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

    public async Task Consume(ConsumeContext<DeleteOrderRequest> context)
    {
        DeleteOrderResponse response = new()
        {
            IsDeleted = await _command.Execute(context.Message.OrderId),
        };
        
       await context.RespondAsync(response);
    }
}