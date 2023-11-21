using Core.Requests.Customers;
using Core.Responses.Customers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Customers;

public class UpdateCustomerMessagePublisher : IMessagePublisher<UpdateCustomerRequest, UpdateCustomerResponse>
{
    private readonly IRequestClient<UpdateCustomerRequest> _requestClient;

    public UpdateCustomerMessagePublisher([FromServices] IRequestClient<UpdateCustomerRequest> requestClient)
    {
        _requestClient = requestClient;
    }
    public UpdateCustomerResponse SendMessage(UpdateCustomerRequest request)
    {
        return _requestClient.GetResponse<UpdateCustomerResponse>(request).Result.Message;
    }
}