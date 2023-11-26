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

    public async Task<DeleteOrderResponse> SendMessageAsync(DeleteOrderRequest request)
    {
        return (await _requestClient.GetResponse<DeleteOrderResponse>(request)).Message;
    }
}