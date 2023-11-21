namespace Core.Requests.Products;

public class CreateProductRequest
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
}