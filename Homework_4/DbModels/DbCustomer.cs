namespace Homework_4.DbModels;

public class DbCustomer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<DbOrder> Orders { get; set; }
}