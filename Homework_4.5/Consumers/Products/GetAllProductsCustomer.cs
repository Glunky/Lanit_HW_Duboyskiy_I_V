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

    public async Task Consume(ConsumeContext<GetAllProductsRequest> context)
    {
       await context.RespondAsync(await _command.Execute());
    }
}