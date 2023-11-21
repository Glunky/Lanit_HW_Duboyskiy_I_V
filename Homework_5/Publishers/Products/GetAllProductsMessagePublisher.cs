using Core.Requests.Products;
using Core.Responses.Products;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Products;

public class GetAllProductsMessagePublisher : IMessagePublisher<GetAllProductsRequest, GetAllProductsResponse>
{
    private readonly IRequestClient<GetAllProductsRequest> _requestClient;

    public GetAllProductsMessagePublisher([FromServices] IRequestClient<GetAllProductsRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public GetAllProductsResponse SendMessage(GetAllProductsRequest request)
    {
        return _requestClient.GetResponse<GetAllProductsResponse>(request).Result.Message;
    }
}