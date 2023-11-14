using Homework_4.DbModels;
using Homework_4.Repositories;

namespace Homework_4.Controllers;

public class OrdersController : IController
{
    private readonly OrderRepository _orderRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly ProductRepository _productRepository;
    
    public OrdersController(
        OrderRepository orderRepository, 
        CustomerRepository customerRepository,
        ProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }
    
    public async Task CreateItem()
    {
        var customers = await _customerRepository.ReadAllCustomers();
        DateTime date = DateTime.Today;

        for (int i = 0; i < customers.Length; i++)
        {
            Console.WriteLine($"--- {i}. FirstName={customers[i].FirstName}, LastName={customers[i].LastName} ---");
        }
        
        Console.Write("Выберите покупателя: ");

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int selectedCustomerIdx) && 
                selectedCustomerIdx >= 0 && selectedCustomerIdx < customers.Length)
            {
                var selectedCustomer = customers[selectedCustomerIdx];

                Guid orderId = Guid.NewGuid();
                DbOrder order = new DbOrder { Id = orderId };
                
                bool isOrderCreated = false;

                while (true)
                {
                    var products = await _productRepository.ReadAllProducts();

                    for (int i = 0; i < products.Length; i++)
                    {
                        var product = products[i];
                        Console.WriteLine($"--- {i}. ProductName={product.ProductName}, Price={product.Price} ---");
                    }
                    
                    Console.Write("Выберите продукт: ");

                    if (int.TryParse(Console.ReadLine(), out int selectedProductIdx) &&
                        selectedProductIdx >= 0 && selectedProductIdx < products.Length)
                    {
                        var selectedProduct = products[selectedProductIdx];

                        if (!isOrderCreated)
                        {
                            order.CustomerId = selectedCustomer.Id;
                            order.Date = date;
                            
                            await _orderRepository.CreateOrder(order);

                            Console.WriteLine($"Заказ для пользователя {selectedCustomer.Id} успешно создан");

                            isOrderCreated = true;
                        }

                        order.Products.Add(selectedProduct);
                        
                        Console.WriteLine($"Продукт {selectedProduct.ProductName} добавлен в заказ");
                        Console.WriteLine("Желаете добавить ещё продуктов в заказ? y/n");

                        ConsoleKey inputKey = Console.ReadKey().Key;

                        if (inputKey == ConsoleKey.Y)
                        {
                            continue;
                        }

                        if (inputKey == ConsoleKey.N)
                        {
                            await _orderRepository.SaveChangesAsync();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введены некорректные денные, попробуйте ещё раз");
                    }
                }
            }
            else
            {
                Console.Write("Введены некорректные денные, попробовать езё раз? y/n: ");

                ConsoleKey inputKey = Console.ReadKey().Key;

                if (inputKey == ConsoleKey.Y)
                {
                    continue;
                }

                if (inputKey == ConsoleKey.N)
                {
                    return;
                }
            }
        }
    }

    public async Task ReadAllItems()
    {
        var orders = await _orderRepository.ReadAllOrders();

        for (int i = 0; i < orders.Length; i++)
        {
            var order = orders[i];
            var customer = order.Customer;
            var products = order.Products;
            
            Console.WriteLine($"{i}. Пользователь {customer.FirstName} в заказе от {order.Date} №{order.Id} заказывал: ");
            foreach (var p in products)
            {
                Console.WriteLine($"--- {p.ProductName} ---");
            }
        }
    }

    public async Task ReadItem()
    {
        Console.Write("Введите Id заказа для чтения: ");
        
        if (Guid.TryParse(Console.ReadLine(), out Guid customerId))
        {
            var order = await _orderRepository.ReadOrder(customerId);

            Console.WriteLine(order != null ? $"--- Заказ № {order.Id} был совершён {order.Date} ---" : "Такого заказа не существует");
        }
    
        Console.Write("Некорректно введены данные");
    }

    public async Task UpdateItem()
    {
        var orders = await _orderRepository.ReadAllOrders();
        
        while (true)
        {
            for (int i = 0; i < orders.Length; i++)
            {
                Console.WriteLine($"--- {i}. Id={orders[i].Id} ---");
            }
            
            Console.Write("Выберите заказ для обновления: ");
            
            if (int.TryParse(Console.ReadLine(), out int selectedOrderIdx) && 
                selectedOrderIdx >= 0 && selectedOrderIdx < orders.Length)
            {
                var selectedOrder = orders[selectedOrderIdx];

                while (true)
                {
                    Console.WriteLine("Введите новую дату заказа: ");

                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    {
                        await _orderRepository.UpdateOrder(selectedOrder, date);
                
                        Console.WriteLine("Пользовательские данные успешно изменёны");
                
                        return;
                    }
                    
                    Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
                }
            }
            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }

    public async Task DeleteItem()
    {
        var orders = await _orderRepository.ReadAllOrders();

        while (true)
        { 
            for (int i = 0; i < orders.Length; i++)
            {
                Console.WriteLine($"--- {i}. FirstName={orders[i].Id} ---");
            }
            
            Console.Write("Выберите заказ для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int selectedOrderIdx) && 
                selectedOrderIdx >= 0 && selectedOrderIdx < orders.Length)
            {
                await _orderRepository.DeleteOrder(orders[selectedOrderIdx]);

                Console.WriteLine("Заказ успешно удалён");

                return;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }
}
