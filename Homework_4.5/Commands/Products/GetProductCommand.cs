using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class GetProductCommand : IGetProductCommand
{
    private readonly IRepository<DbProduct, ProductInfo> _repository;
    private readonly IMapper<DbProduct, ProductInfo> _mapper;
    private readonly IHttpContextAccessor _accessor;

    public GetProductCommand(
        IRepository<DbProduct, ProductInfo> repository, 
        IMapper<DbProduct, ProductInfo> mapper,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _mapper = mapper;
        _accessor = accessor;
    }
    
    public async Task<ResultResponse<ProductInfo>> Execute(Guid id)
    {
        DbProduct product = await _repository.Read(id);

        if (product == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            return new ()
            {
                Errors = new()
                {
                    $"Cannot find product with Id {id}"
                }
            };
        }
            
        return new()
        {
            Body = _mapper.Map(product)
        };
    }
}