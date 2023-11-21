namespace Core.Requests.Products;

public class UpdateProductRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
}