using Core.Requests.Orders;
using Core.Responses.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Orders;

public class DeleteOrderMessagePublisher : IMessagePublisher<DeleteOrderRequest, DeleteOrderResponse>
{
    private readonly IRequestClient<DeleteOrderRequest> _requestClient;

    public DeleteOrderMessagePublisher([FromServices] IRequestClient<DeleteOrderRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public DeleteOrderResponse SendMessage(DeleteOrderRequest request)
    {
        return _requestClient.GetResponse<DeleteOrderResponse>(request).Result.Message;
    }
}