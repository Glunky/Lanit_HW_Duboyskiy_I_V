using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class DeleteOrderCommand : IDeleteOrderCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;

    public DeleteOrderCommand(
        IRepository<DbOrder, OrderInfo> repository
    )
    {
        _repository = repository;
    }
    
    public async Task<bool> Execute(Guid id)
    {
        DbOrder order = await _repository.Read(id);

        if (order == null)
        {
            return false;
        }

        await _repository.Delete(order);

        return true;
    }
}