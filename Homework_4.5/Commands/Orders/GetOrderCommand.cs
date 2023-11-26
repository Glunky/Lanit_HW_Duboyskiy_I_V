using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Mappers;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class GetOrderCommand : IGetOrderCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;
    private readonly IMapper<DbOrder, OrderInfo> _mapper;

    public GetOrderCommand(
        IRepository<DbOrder, OrderInfo> repository, 
        IMapper<DbOrder, OrderInfo> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResultResponse<OrderInfo>> Execute(Guid id)
    {
        DbOrder order = await _repository.Read(id);

        if (order == null)
        {
            return new()
            {
                Errors = new()
                {
                    $"Cannot find order with Id {id}"
                }
            };
        }

        return new()
        {
            Body = _mapper.Map(order)
        };
    }
}