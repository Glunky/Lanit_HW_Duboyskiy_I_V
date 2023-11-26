using Core.Requests.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Validation;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class UpdateOrderCommand : IUpdateOrderCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;
    private readonly IValidator<UpdateOrderRequest> _validator;

    public UpdateOrderCommand(
        IRepository<DbOrder, OrderInfo> repository,
        IValidator<UpdateOrderRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<bool> Execute(UpdateOrderRequest info)
    {
        ValidationResult validationResult = _validator.Validate(info);

        if (!validationResult.IsValid)
        {
            return false;
        }
        
        DbOrder order = await _repository.Read(info.OrderId);

        await _repository.Update(order, new OrderInfo()
        {
            CustomerId = info.CustomerId,
            Date = info.Date,
            ProductsIds = info.ProductsIds
        });

        return true;
    }
}