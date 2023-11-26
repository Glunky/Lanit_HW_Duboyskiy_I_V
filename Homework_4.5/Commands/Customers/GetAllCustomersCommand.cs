using Core.Requests;
using Core.Requests.Customers;
using Core.Responses;
using Core.Responses.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Customers;

public class GetAllCustomersCommand : IGetAllCustomersCommand
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    private readonly IMapper<DbCustomer, GetCustomerResponse> _mapper;

    public GetAllCustomersCommand(
        IRepository<DbCustomer, CustomerInfo> repository,
        IMapper<DbCustomer, GetCustomerResponse> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetAllCustomersResponse> Execute()
    {
        return new()
        {
            GetCustomerResponses = (await _repository.ReadAll()).Select(c => _mapper.Map(c))
        };
    }
}
