using Core.Responses;
using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories.Interfaces;

namespace Homework_4._5.Commands.Products;

public class DeleteProductCommand : IDeleteProductCommand
{
    private IRepository<DbProduct, ProductInfo> _repository;

    public DeleteProductCommand(
        IRepository<DbProduct, ProductInfo> repository
    )
    {
        _repository = repository;
    }
    
    public async Task<bool> Execute(Guid id)
    {
        DbProduct product = await _repository.Read(id);

        if (product == null)
        {
            return false;
        }

        await _repository.Delete(product);

        return true;
    }
}