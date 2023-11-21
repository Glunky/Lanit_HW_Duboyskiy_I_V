using Core.Requests.Products;
using Core.Responses.Products;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Products;

public class UpdateProductMessagePublisher : IMessagePublisher<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IRequestClient<UpdateProductRequest> _requestClient;

    public UpdateProductMessagePublisher([FromServices] IRequestClient<UpdateProductRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public UpdateProductResponse SendMessage(UpdateProductRequest request)
    {
        return _requestClient.GetResponse<UpdateProductResponse>(request).Result.Message;
    }
}