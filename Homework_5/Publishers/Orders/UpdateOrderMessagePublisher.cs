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

    public async Task<UpdateOrderResponse> SendMessageAsync(UpdateOrderRequest request)
    {
        return (await _requestClient.GetResponse<UpdateOrderResponse>(request)).Message;
    }
}