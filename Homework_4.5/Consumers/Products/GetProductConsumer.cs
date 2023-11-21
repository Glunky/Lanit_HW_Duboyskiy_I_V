using Core.Requests.Products;
using Homework_4._5.Commands.Products.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Products;

public class GetProductConsumer : IConsumer<GetProductRequest>
{
    private readonly IGetProductCommand _command;

    public GetProductConsumer(IGetProductCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<GetProductRequest> context)
    {
        context.Respond(_command.Execute(context.Message.ProductId).Result);

        return Task.CompletedTask;
    }
}
