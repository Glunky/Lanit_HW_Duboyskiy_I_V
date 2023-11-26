using Core.Responses.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Mappers;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Orders;

public class GetAllOrdersCommand : IGetAllOrdersCommand
{
    private readonly IRepository<DbOrder, OrderInfo> _repository;
    private readonly IMapper<DbOrder, GetOrderResponse> _mapper;

    public GetAllOrdersCommand(
        IRepository<DbOrder, OrderInfo> repository,
        IMapper<DbOrder, GetOrderResponse> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetAllOrdersResponse> Execute()
    {
        return new()
        {
            Responses = (await _repository.ReadAll()).Select(c => _mapper.Map(c))
        };
    }
}