namespace Core.Responses.Customers;

public class GetAllCustomersResponse
{
    public IEnumerable<GetCustomerResponse> GetCustomerResponses { get; set; }
}