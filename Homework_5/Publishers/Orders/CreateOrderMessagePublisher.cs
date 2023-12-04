using Core.Requests.Orders;
using Core.Responses.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Orders;

public class CreateOrderMessagePublisher : IMessagePublisher<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IRequestClient<CreateOrderRequest> _requestClient;

    public CreateOrderMessagePublisher([FromServices] IRequestClient<CreateOrderRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<CreateOrderResponse> SendMessageAsync(CreateOrderRequest request)
    {
        return (await _requestClient.GetResponse<CreateOrderResponse>(request)).Message;
    }
}