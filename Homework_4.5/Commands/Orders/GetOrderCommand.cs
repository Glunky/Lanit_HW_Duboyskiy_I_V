using Core.Responses;
using Core.Responses.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Mappers;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class GetOrderCommand : IGetOrderCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;
    private readonly IMapper<DbOrder, GetOrderResponse> _mapper;

    public GetOrderCommand(
        IRepository<DbOrder, OrderInfo> repository, 
        IMapper<DbOrder, GetOrderResponse> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<GetOrderResponse> Execute(Guid id)
    {
        DbOrder order = await _repository.Read(id);

        if (order == null)
        {
            return null;
        }

        var x = _mapper.Map(order);
        return x;
    }
}