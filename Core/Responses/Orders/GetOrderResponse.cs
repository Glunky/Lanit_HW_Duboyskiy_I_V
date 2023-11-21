namespace Core.Responses.Orders;

public class GetOrderResponse
{
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public List<Guid> ProductsIds { get; set; }
}