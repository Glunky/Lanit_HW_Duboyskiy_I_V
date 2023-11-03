namespace Homework_4.DbModels;

public class DbOrder
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public DbCustomer Customer { get; set; }
    public List<DbOrderProduct> OrderProducts { get; set; }
}