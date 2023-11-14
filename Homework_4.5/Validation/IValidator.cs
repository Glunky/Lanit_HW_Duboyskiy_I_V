namespace Homework_4._5.Validation;

public interface IValidator<T>
{
    ValidationResult Validate(T request);
}