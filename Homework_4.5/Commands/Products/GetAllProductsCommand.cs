using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class GetAllProductsCommand : IGetAllProductsCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IMapper<DbProduct, ProductInfo> _mapper;

    public GetAllProductsCommand(
        IRepository<DbProduct, ProductInfo> repository,
        IMapper<DbProduct, ProductInfo> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResultResponse<IEnumerable<ProductInfo>>> Execute()
    {
        return new()
        {
            Body = (await _repository.ReadAll()).Select(c => _mapper.Map(c))
        };
    }
}