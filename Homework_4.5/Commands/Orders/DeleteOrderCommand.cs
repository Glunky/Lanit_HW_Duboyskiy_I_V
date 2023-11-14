using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class DeleteOrderCommand : IDeleteOrderCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;
    private readonly IHttpContextAccessor _accessor;

    public DeleteOrderCommand(
        IRepository<DbOrder, OrderInfo> repository,
        IHttpContextAccessor accessor
    )
    {
        _repository = repository;
        _accessor = accessor;
    }
    
    public async Task<ResultResponse<bool>> Execute(Guid id)
    {
        DbOrder order = await _repository.Read(id);

        if (order == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new()
                {
                    $"Cannot find order with Id {id}"
                }
            };
        }

        await _repository.Delete(order);

        return new()
        {
            Body = true
        };
    }
}