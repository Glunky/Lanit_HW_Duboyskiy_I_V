using Core.Requests.Customers;
using Core.Responses.Customers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Customers;

public class GetCustomerMessagePublisher : IMessagePublisher<GetCustomerRequest, GetCustomerResponse>
{
    private readonly IRequestClient<GetCustomerRequest> _requestClient;

    public GetCustomerMessagePublisher([FromServices] IRequestClient<GetCustomerRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public GetCustomerResponse SendMessage(GetCustomerRequest request)
    {
        return _requestClient.GetResponse<GetCustomerResponse>(request).Result.Message;
    }
}
