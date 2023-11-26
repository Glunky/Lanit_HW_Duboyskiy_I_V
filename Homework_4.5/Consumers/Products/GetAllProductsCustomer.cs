using Core.Requests.Products;
using Homework_4._5.Commands.Products.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Products;

public class GetAllProductsConsumer : IConsumer<GetAllProductsRequest>
{
    private readonly IGetAllProductsCommand _command;

    public GetAllProductsConsumer(IGetAllProductsCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<GetAllProductsRequest> context)
    {
        context.Respond(_command.Execute().Result);

        return Task.CompletedTask;
    }
}