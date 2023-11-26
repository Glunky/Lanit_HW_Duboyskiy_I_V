using Core.Requests.Orders;
using Core.Responses.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Orders;

public class GetOrderMessagePublisher : IMessagePublisher<GetOrderRequest, GetOrderResponse>
{
    private readonly IRequestClient<GetOrderRequest> _requestClient;

    public GetOrderMessagePublisher([FromServices] IRequestClient<GetOrderRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public GetOrderResponse SendMessage(GetOrderRequest request)
    {
        return _requestClient.GetResponse<GetOrderResponse>(request).Result.Message;
    }
}