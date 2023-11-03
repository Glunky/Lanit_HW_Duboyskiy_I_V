namespace Homework_4.DbModels;

public class DbOrderProduct
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DbOrder Order { get; set; }
    public DbProduct Product { get; set; }
}