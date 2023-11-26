namespace Homework_4._5.Controllers;

public class OrderInfo
{
    public Guid? Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public List<Guid> ProductsIds { get; set; }
}