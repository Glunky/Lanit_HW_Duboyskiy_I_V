using Core.Requests.Products;
using Core.Responses.Products;
using Homework_4._5.Commands.Products.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4._5.Consumers.Products;

public class UpdateProductConsumer : IConsumer<UpdateProductRequest>
{
    private readonly IUpdateProductCommand _command;

    public UpdateProductConsumer([FromServices] IUpdateProductCommand command)
    {
        _command = command;
    }

    public Task Consume(ConsumeContext<UpdateProductRequest> context)
    {
        context.Respond(new UpdateProductResponse()
        {
            IsUpdated = _command.Execute(context.Message).Result
        });

        return Task.CompletedTask;
    }
}