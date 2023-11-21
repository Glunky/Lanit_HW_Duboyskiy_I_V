using Core.Requests.Products;
using Core.Responses.Customers;
using Core.Responses.Products;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Products;

public class CreateProductMessagePublisher : IMessagePublisher<CreateProductRequest, CreateProductResponse>
{
    private readonly IRequestClient<CreateProductRequest> _requestClient;

    public CreateProductMessagePublisher([FromServices] IRequestClient<CreateProductRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public CreateProductResponse SendMessage(CreateProductRequest request)
    {
        return _requestClient.GetResponse<CreateProductResponse>(request).Result.Message;
    }
}