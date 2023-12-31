﻿using Core.Requests;
using Core.Requests.Customers;
using Homework_4._5.Controllers;
using Homework_4._5.Requests;
using Homework_4.Controllers;
using Homework_4.DbModels;
using Homework_4.Provider;
using Homework_4.Repositories;
using Homework_4.Repositories.Interfaces;

namespace Homework_4;

internal class Program
{
    static async Task Main(string[] args)
    {
        await using PurchaseDbContext dbProvider = new PurchaseDbContext();
        
        await dbProvider.Database.EnsureCreatedAsync();

        IRepository<DbCustomer, CustomerInfo> customerRepository = new CustomerRepository(dbProvider);
        IRepository<DbProduct, ProductInfo> productRepository = new ProductRepository(dbProvider);
        IRepository<DbOrder, OrderInfo> orderRepository = new OrderRepository(dbProvider);

        IController customersController = new CustomersController(customerRepository);
        IController productsController = new ProductsController(productRepository);
        IController ordersController = new OrdersController(orderRepository, customerRepository, productRepository);

        Dictionary<Constants.DbEntities, Func<Task>> createDbEntitiesImplementation = new()
        {
            { Constants.DbEntities.Product, productsController.CreateItem },
            { Constants.DbEntities.Customer, customersController.CreateItem },
            { Constants.DbEntities.Order, ordersController.CreateItem },
        };

        Dictionary<Constants.DbEntities, Func<Task>> readDbEntitiesImplementation = new()
        {
            { Constants.DbEntities.Product, productsController.ReadAllItems },
            { Constants.DbEntities.Customer, customersController.ReadAllItems },
            { Constants.DbEntities.Order, ordersController.ReadAllItems }
        };
        
        Dictionary<Constants.DbEntities, Func<Task>> readSingleDbEntityImplementation = new()
        {
            { Constants.DbEntities.Product, productsController.ReadItem },
            { Constants.DbEntities.Customer, customersController.ReadItem },
            { Constants.DbEntities.Order, ordersController.ReadItem }
        };

        Dictionary<Constants.DbEntities, Func<Task>> updateDbEntitiesImplementation = new()
        {
            { Constants.DbEntities.Product, productsController.UpdateItem },
            { Constants.DbEntities.Customer, customersController.UpdateItem },
            { Constants.DbEntities.Order, ordersController.UpdateItem },
        };

        Dictionary<Constants.DbEntities, Func<Task>> deleteDbEntitiesImplementation = new()
        {
            { Constants.DbEntities.Product, productsController.DeleteItem },
            { Constants.DbEntities.Customer, customersController.DeleteItem },
            { Constants.DbEntities.Order, ordersController.DeleteItem },
        };

        Dictionary<Constants.CRUD, Dictionary<Constants.DbEntities, Func<Task>>> CRUDtoEntitiesMap = new()
        {
            { Constants.CRUD.Create, createDbEntitiesImplementation },
            { Constants.CRUD.Read, readDbEntitiesImplementation },
            { Constants.CRUD.ReadSingle, readSingleDbEntityImplementation },
            { Constants.CRUD.Update, updateDbEntitiesImplementation },
            { Constants.CRUD.Delete, deleteDbEntitiesImplementation },
        };

        while (true)
        {
            Console.WriteLine($"{(int)Constants.CRUD.Create}. {Constants.CRUD.Create.ToString()}");
            Console.WriteLine($"{(int)Constants.CRUD.Read}. {Constants.CRUD.Read.ToString()}");
            Console.WriteLine($"{(int)Constants.CRUD.Update}. {Constants.CRUD.Update.ToString()}");
            Console.WriteLine($"{(int)Constants.CRUD.Delete}. {Constants.CRUD.Delete.ToString()}");

            Console.Write("Выберите операцию: ");

            if (int.TryParse(Console.ReadLine(), out int selectedOperation) && 
                CRUDtoEntitiesMap.TryGetValue((Constants.CRUD)selectedOperation, out var entitiesMap))
            {
                await CRUDtemplate(entitiesMap);

                while (true)
                {
                    Console.WriteLine("Операция закончена, завершить программу? y/n: ");

                    var inputKey = Console.ReadKey().Key;
                    
                    if (inputKey == ConsoleKey.Y)
                    {
                        return;
                    }
                    
                    if (inputKey == ConsoleKey.N)
                    {
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Данные введены некорректно, попробовать ещё раз? y/n: ");

                    var inputKey = Console.ReadKey().Key;
                    
                    if (inputKey == ConsoleKey.Y)
                    {
                        break;
                    }
                    
                    if (inputKey == ConsoleKey.N)
                    {
                        return;
                    }
                }
            }
        }
    }
    
    static async Task CRUDtemplate(Dictionary<Constants.DbEntities, Func<Task>> entityImpementation)
    {
        while (true)
        {
            Console.WriteLine($"{(int)Constants.DbEntities.Customer}. {Constants.DbEntities.Customer.ToString()}");
            Console.WriteLine($"{(int)Constants.DbEntities.Product}. {Constants.DbEntities.Product.ToString()}");
            Console.WriteLine($"{(int)Constants.DbEntities.Order}. {Constants.DbEntities.Order.ToString()}");

            Console.Write("Выберите сущность: ");

            if (int.TryParse(Console.ReadLine(), out int selectedEntity) &&
                entityImpementation.TryGetValue((Constants.DbEntities)selectedEntity, out var entityOperation))
            {
                await entityOperation();

                while (true)
                {
                    Console.WriteLine("Операция закончена, повторить операцию? y/n: ");

                    var inputKey = Console.ReadKey().Key;
                    
                    if (inputKey == ConsoleKey.Y)
                    {
                        break;
                    }
                    
                    if (inputKey == ConsoleKey.N)
                    {
                        return;
                    }
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Данные введены некорректно, попробовать ещё раз? y/n: ");

                    var inputKey = Console.ReadKey().Key;
                    
                    if (inputKey == ConsoleKey.Y)
                    {
                        break;
                    }
                    
                    if (inputKey == ConsoleKey.N)
                    {
                        return;
                    }
                }
            }
        }
    }
}