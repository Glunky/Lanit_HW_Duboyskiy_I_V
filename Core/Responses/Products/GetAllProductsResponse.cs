namespace Core.Responses.Products;

public class GetAllProductsResponse
{
    public IEnumerable<GetProductResponse> GetProductResponses { get; set; }
}