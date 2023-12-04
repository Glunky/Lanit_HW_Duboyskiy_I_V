using Core.Requests.Customers;
using Core.Responses.Customers;
using MassTransit;

namespace Homework_5.Publishers.Customers;

public class CreateCustomerMessagePublisher : IMessagePublisher<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly IRequestClient<CreateCustomerRequest> _requestClient;

    public CreateCustomerMessagePublisher(IRequestClient<CreateCustomerRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<CreateCustomerResponse> SendMessageAsync(CreateCustomerRequest request)
    {
        return (await _requestClient.GetResponse<CreateCustomerResponse>(request)).Message;
    }
}