using Core.Requests.Products;

namespace Homework_4._5.Validation.Products;

public class CreateProductValidator : IValidator<CreateProductRequest>
{
    public ValidationResult Validate(CreateProductRequest request)
    {
        ValidationResult result = new();
        
        if (request == null)
        {
            result.Errors.Add("Bad request");
        }

        if (request.ProductName.Length > 50)
        {
            result.Errors.Add("Product name is too long");
        }
        
        if (request.ProductPrice < 0)
        {
            result.Errors.Add("Price product cannot be negative");
        }

        if (result.Errors.Count == 0)
        {
            result.IsValid = true;
        }

        return result;
    }
}