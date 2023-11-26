using Core.Requests.Products;

namespace Homework_4._5.Validation.Products;

public class UpdateProductValidator : IValidator<UpdateProductRequest>
{
    public ValidationResult Validate(UpdateProductRequest request)
    {
        ValidationResult result = new();
        
        if (request == null || request.ProductId == default)
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