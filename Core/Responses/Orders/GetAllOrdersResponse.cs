namespace Core.Responses.Orders;

public class GetAllOrdersResponse
{
    public IEnumerable<GetOrderResponse> Responses { get; set; }
}