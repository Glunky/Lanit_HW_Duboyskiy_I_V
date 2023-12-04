using Core.Requests.Products;
using Core.Responses.Products;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Products;

public class GetProductMessagePublisher : IMessagePublisher<GetProductRequest, GetProductResponse>
{
    private readonly IRequestClient<GetProductRequest> _requestClient;

    public GetProductMessagePublisher([FromServices] IRequestClient<GetProductRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<GetProductResponse> SendMessageAsync(GetProductRequest request)
    {
        return (await _requestClient.GetResponse<GetProductResponse>(request)).Message;
    }
}