namespace Core.Requests.Orders;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public List<Guid> ProductsIds { get; set; }
}