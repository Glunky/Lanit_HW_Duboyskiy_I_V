using Core.Requests;
using Core.Requests.Customers;

namespace Homework_4._5.Validation.Customers;

public class UpdateCustomerValidator : IValidator<UpdateCustomerRequest>
{
    public ValidationResult Validate(UpdateCustomerRequest request)
    {
        ValidationResult result = new();
        
        if (request == null || request.Id == default)
        {
            result.Errors.Add("Bad request");
        }

        if (request.FirstName.Length > 50)
        {
            result.Errors.Add("First name is too long");
        }

        if (request.LastName.Length > 50)
        {
            result.Errors.Add("Las name is too long");
        }

        if (result.Errors.Count == 0)
        {
            result.IsValid = true;
        }

        return result;
    }
}