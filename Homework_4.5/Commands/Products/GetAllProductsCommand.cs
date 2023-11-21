using Core.Responses;
using Core.Responses.Products;
using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class GetAllProductsCommand : IGetAllProductsCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IMapper<DbProduct, GetProductResponse> _mapper;

    public GetAllProductsCommand(
        IRepository<DbProduct, ProductInfo> repository,
        IMapper<DbProduct, GetProductResponse> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetAllProductsResponse> Execute()
    {
        return new()
        {
            GetProductResponses = (await _repository.ReadAll()).Select(c => _mapper.Map(c))
        };
    }
}