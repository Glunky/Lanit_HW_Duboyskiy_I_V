using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Requests;
using Homework_4._5.Responces;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class DeleteProductCommand : IDeleteProductCommand
{
    private IRepository<DbProduct, ProductInfo> _repository;
    private readonly IHttpContextAccessor _accessor;

    public DeleteProductCommand(
        IRepository<DbProduct, ProductInfo> repository,
        IHttpContextAccessor accessor
    )
    {
        _repository = repository;
        _accessor = accessor;
    }
    
    public async Task<ResultResponse<bool>> Execute(Guid id)
    {
        DbProduct product = await _repository.Read(id);

        if (product == null)
        {
            _accessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new()
            {
                Errors = new()
                {
                    $"Cannot find product with Id {id}"
                }
            };
        }

        await _repository.Delete(product);

        return new()
        {
            Body = true
        };
    }
}