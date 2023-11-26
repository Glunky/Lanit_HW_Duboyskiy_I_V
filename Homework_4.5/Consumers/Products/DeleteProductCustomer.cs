using Core.Requests.Products;
using Core.Responses.Products;
using Homework_4._5.Commands.Products.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Consumers.Products;

public class DeleteProductConsumer : IConsumer<DeleteProductRequest>
{
    private readonly IDeleteProductCommand _command;

    public DeleteProductConsumer([FromServices] IDeleteProductCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<DeleteProductRequest> context)
    {
        DeleteProductResponse response = new()
        {
            IsDeleted = _command.Execute(context.Message.ProductId).Result,
        };
        
        context.Respond(response);

        return Task.CompletedTask;
    }
}