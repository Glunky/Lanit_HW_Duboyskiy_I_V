namespace Core.Requests.Customers;

public class UpdateCustomerRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}