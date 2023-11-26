using Core.Requests.Products;
using Core.Responses.Products;
using Homework_4._5.Commands.Products.Interfaces;
using MassTransit;

namespace Homework_4._5.Consumers.Products;

public class CreateProductConsumer : IConsumer<CreateProductRequest>
{
    private readonly ICreateProductCommand _command;

    public CreateProductConsumer(ICreateProductCommand command)
    {
        _command = command;
    }

    public async Task Consume(ConsumeContext<CreateProductRequest> context)
    {
       await context.RespondAsync(new CreateProductResponse()
        {
            ProductId = await _command.Execute(context.Message)
        });
    }
}