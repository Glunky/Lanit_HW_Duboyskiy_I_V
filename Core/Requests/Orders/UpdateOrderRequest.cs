namespace Core.Requests.Orders;

public class UpdateOrderRequest
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public List<Guid> ProductsIds { get; set; }
}