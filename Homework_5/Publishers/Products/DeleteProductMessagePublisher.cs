using Core.Requests.Products;
using Core.Responses.Products;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Products;

public class DeleteProductMessagePublisher : IMessagePublisher<DeleteProductRequest, DeleteProductResponse>
{
    private readonly IRequestClient<DeleteProductRequest> _requestClient;

    public DeleteProductMessagePublisher([FromServices] IRequestClient<DeleteProductRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public DeleteProductResponse SendMessage(DeleteProductRequest request)
    {
        return _requestClient.GetResponse<DeleteProductResponse>(request).Result.Message;
    }
}