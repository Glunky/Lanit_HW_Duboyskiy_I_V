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

    public GetAllCustomersResponse SendMessage(GetAllCustomersRequest request)
    {
        return _requestClient.GetResponse<GetAllCustomersResponse>(request).Result.Message;
    }
}