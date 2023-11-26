using Core.Requests.Orders;
using Core.Responses.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Orders;

public class GetAllOrdersMessagePublisher : IMessagePublisher<GetAllOrdersRequest, GetAllOrdersResponse>
{
    private readonly IRequestClient<GetAllOrdersRequest> _requestClient;

    public GetAllOrdersMessagePublisher([FromServices] IRequestClient<GetAllOrdersRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<GetAllOrdersResponse> SendMessageAsync(GetAllOrdersRequest request)
    {
        return (await _requestClient.GetResponse<GetAllOrdersResponse>(request)).Message;
    }
}