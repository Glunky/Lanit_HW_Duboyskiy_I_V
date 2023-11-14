using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Repositories;
using Homework_4.Repositories.Interfaces;

namespace Homework_4.Controllers;

public class CustomersController : IController
{
    private readonly IRepository<DbCustomer, CustomerInfo> _repository;
    
    public CustomersController(IRepository<DbCustomer, CustomerInfo> repository)
    {
        _repository = repository;
    }
    
    public async Task CreateItem()
    {
        Console.Write("Введите имя покупателя: ");
        string customerFirstName = Console.ReadLine();

        Console.Write("Введите фамилию покупателя: ");
        string customerLastName = Console.ReadLine();

        await _repository.Create(new DbCustomer
        {
            FirstName = customerFirstName,
            LastName = customerLastName
        });

        Console.WriteLine($"Пользователь {customerFirstName} успешно создан");
    }

    public async Task ReadAllItems()
    {
        foreach (var customer in await _repository.ReadAll())
        {
            Console.WriteLine($"--- FirstName={customer.FirstName}, LastName={customer.LastName} ---");
        }
    }

    public async Task ReadItem()
    {
        Console.Write("Введите Id пользователя для чтения: ");
            
        if (Guid.TryParse(Console.ReadLine(), out Guid customerId))
        {
            var customer = await _repository.Read(customerId);

            Console.WriteLine(customer != null ? $"--- FirstName={customer.FirstName}, LastName={customer.LastName} ---" : "Такого пользователя не существует");
        }
        
        Console.Write("Некорректно введены данные");
    }

    public async Task UpdateItem()
    {
        var customers = await _repository.ReadAll();
        
        while (true)
        {
            for (int i = 0; i < customers.Length; i++)
            {
                Console.WriteLine($"--- {i}. FirstName={customers[i].FirstName}, LastName={customers[i].LastName} ---");
            }
            
            Console.Write("Выберите пользователя для обновления: ");
            
            if (int.TryParse(Console.ReadLine(), out int selectedCustomerIdx) && 
                selectedCustomerIdx >= 0 && selectedCustomerIdx < customers.Length)
            {
                var selectedCustomer = customers[selectedCustomerIdx];
                
                Console.Write("Введите новое имя покупателя: ");
                string customerFirstName = Console.ReadLine();
        
                Console.Write("Введите новую фамилию покупателя: ");
                string customerLastName = Console.ReadLine();

                await _repository.Update(selectedCustomer, new CustomerInfo
                {
                    FirstName = customerFirstName,
                    LastName = customerLastName
                });
                
                Console.WriteLine("Пользовательские данные успешно изменёны");
                
                return;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }

    public async Task DeleteItem()
    {
        var customers = await _repository.ReadAll();
        
        while (true)
        {
            for (int i = 0; i < customers.Length; i++)
            {
                var customer = customers[i];
                Console.WriteLine($"--- {i}. FirstName={customer.FirstName}, LastName={customer.LastName} ---");
            }
            
            Console.Write("Выберите пользователя для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int selectedCustomerIdx) && 
                selectedCustomerIdx >= 0 && selectedCustomerIdx < customers.Length)
            {
                await _repository.Delete(customers[selectedCustomerIdx]);
                
                Console.WriteLine("Пользователь успешно удалён");

                return;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }
}
