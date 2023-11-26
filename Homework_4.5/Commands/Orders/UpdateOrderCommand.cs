using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Responces;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class UpdateOrderCommand : IUpdateOrderCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;
    private readonly IValidator<OrderInfo> _validator;
    private readonly IHttpContextAccessor _accessor;

    public UpdateOrderCommand(
        IRepository<DbOrder, OrderInfo> repository,
        IValidator<OrderInfo> validator,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _validator = validator;
        _accessor = accessor;
    }
    public async Task<ResultResponse<bool>> Execute(OrderInfo info)
    {
        ValidationResult validationResult = _validator.Validate(info);

        if (!validationResult.IsValid)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = validationResult.Errors
            };
        }
        
        DbOrder order = await _repository.Read(info.Id.Value);

        await _repository.Update(order, info);

        return new()
        {
            Body = true
        };
    }
}