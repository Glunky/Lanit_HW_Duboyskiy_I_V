using Core.Requests.Orders;
using Core.Responses.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Orders;

public class UpdateOrderMessagePublisher : IMessagePublisher<UpdateOrderRequest, UpdateOrderResponse>
{
    private readonly IRequestClient<UpdateOrderRequest> _requestClient;

    public UpdateOrderMessagePublisher([FromServices] IRequestClient<UpdateOrderRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public UpdateOrderResponse SendMessage(UpdateOrderRequest request)
    {
        return _requestClient.GetResponse<UpdateOrderResponse>(request).Result.Message;
    }
}