using Core.Requests.Customers;
using Core.Responses.Customers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Homework_5.Publishers.Customers;

public class DeleteCustomerMessagePublisher : IMessagePublisher<DeleteCustomerRequest, DeleteCustomerResponse>
{
    private readonly IRequestClient<DeleteCustomerRequest> _requestClient;

    public DeleteCustomerMessagePublisher([FromServices] IRequestClient<DeleteCustomerRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<DeleteCustomerResponse> SendMessageAsync(DeleteCustomerRequest request)
    {
        return (await _requestClient.GetResponse<DeleteCustomerResponse>(request)).Message;
    }
}