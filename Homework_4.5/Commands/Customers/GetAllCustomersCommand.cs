using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers;

public class GetAllCustomersCommand : IGetAllCustomersCommand
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IMapper<DbCustomer, CustomerInfo> _mapper;

    public GetAllCustomersCommand(
        IRepository<DbCustomer, CustomerInfo> repository,
        IMapper<DbCustomer, CustomerInfo> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResultResponse<IEnumerable<CustomerInfo>>> Execute()
    {
        return new()
        {
            Body = (await _repository.ReadAll()).Select(c => _mapper.Map(c))
        };
    }
}