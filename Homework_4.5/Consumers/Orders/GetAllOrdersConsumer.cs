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

    public async Task Consume(ConsumeContext<GetAllOrdersRequest> context)
    {
       await context.RespondAsync(await _command.Execute());
    }
}