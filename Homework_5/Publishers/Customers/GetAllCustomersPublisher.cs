using Core.Requests.Customers;
using Core.Responses.Customers;
using MassTransit;

namespace Homework_5.Publishers.Customers;

public class GetAllCustomersPublisher : IMessagePublisher<GetAllCustomersRequest, GetAllCustomersResponse>
{
    private readonly IRequestClient<GetAllCustomersRequest> _requestClient;

    public GetAllCustomersPublisher(IRequestClient<GetAllCustomersRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<GetAllCustomersResponse> SendMessageAsync(GetAllCustomersRequest request)
    {
        return (await _requestClient.GetResponse<GetAllCustomersResponse>(request)).Message;
    }
}