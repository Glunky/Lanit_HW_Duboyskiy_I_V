using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories;
using Homework_4.Repositories.Interfaces;

namespace Homework_4.Controllers;

public class ProductsController : IController
{
    private IRepository<DbProduct, ProductInfo> _repository;
    
    public ProductsController(IRepository<DbProduct, ProductInfo>  repository)
    {
        _repository = repository;
    }
    
    public async Task CreateItem()
    {
        Console.Write("Введите название продукта: ");
        string productName = Console.ReadLine();
        
        Console.Write("Введите цену продукта: ");
        int productPrice;
        
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out productPrice) && productPrice > 0)
            {
                break;
            }
            
            Console.Write("Некорректные данные, попробуйте ещё раз: ");
        }

        await _repository.Create(new DbProduct()
        {
            ProductName = productName,
            Price = productPrice
        });

        Console.WriteLine($"Продукт {productName} успешно создан");
    }

    public async Task ReadAllItems()
    {
        foreach (var product in await _repository.ReadAll())
        {
            Console.WriteLine($"--- ProductName={product.ProductName}, Price={product.Price} ---");
        }
    }

    public async Task ReadItem()
    {
        Console.Write("Введите Id продукта для чтения: ");
        
        if (Guid.TryParse(Console.ReadLine(), out Guid customerId))
        {
            var product = await _repository.Read(customerId);

            Console.WriteLine(product != null ? $"--- ProductName={product.ProductName}, Price={product.Price} ---" : "Такого продукта не существует");
        }
    
        Console.Write("Некорректно введены данные");
    }

    public async Task UpdateItem()
    {
        while (true)
        {
            var products = await _repository.ReadAll();

            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"--- {i}. ProductName={products[i].ProductName}, Price={products[i].Price} ---");
            }
            
            Console.Write("Выберите продукт для обновления: ");

            if (int.TryParse(Console.ReadLine(), out int selectedProductIdx) && 
                selectedProductIdx >= 0 && selectedProductIdx < products.Length)
            {
                DbProduct selectedProduct = products[selectedProductIdx];

                Console.Write("Введите новое название продукта: ");
                string productName = Console.ReadLine();

                Console.Write("Введите новую цену продукта: ");
                int productPrice;

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out productPrice) && productPrice > 0)
                    {
                        break;
                    }

                    Console.Write("Некорректные данные, попробуйте ещё раз: ");
                }

                await _repository.Update(selectedProduct, new ProductInfo
                {
                    ProductName = productName,
                    Price = productPrice
                });

                Console.WriteLine($"Продукт успешно изменён");

                break;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }

    public async Task DeleteItem()
    {
        var products = await _repository.ReadAll();

        while (true)
        {
            for (int i = 0; i < products.Length; i++)
            {
                var product = products[i];
                
                Console.WriteLine($"--- ProductName={product.ProductName}, Price={product.Price} ---");
            }
            
            Console.Write("Выберите продукт для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int selectedProductIdx) && 
                selectedProductIdx >= 0 && selectedProductIdx < products.Length)
            {
                await _repository.Delete(products[selectedProductIdx]);

                Console.WriteLine("продукт успешно удалён");

                return;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }
}
