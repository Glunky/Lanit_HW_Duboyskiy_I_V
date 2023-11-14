namespace Homework_4.DbModels;

public class DbProduct
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public List<DbOrder> Orders { get; set; }
}