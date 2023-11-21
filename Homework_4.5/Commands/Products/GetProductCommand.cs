using Core.Responses.Products;
using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class GetProductCommand : IGetProductCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IMapper<DbProduct, GetProductResponse> _mapper;

    public GetProductCommand(
        IRepository<DbProduct, ProductInfo> repository, 
        IMapper<DbProduct, GetProductResponse> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<GetProductResponse> Execute(Guid id)
    {
        DbProduct product = await _repository.Read(id);

        if (product == null)
        {
            return null;
        }

        return _mapper.Map(product);
    }
}