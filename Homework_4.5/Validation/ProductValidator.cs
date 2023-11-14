using Homework_4._5.Requests;

namespace Homework_4._5.Validation;

public class ProductValidator : IValidator<ProductInfo>
{
    public ValidationResult Validate(ProductInfo request)
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
        
        if (request.Price < 0)
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