using Microsoft.Data.SqlClient;

namespace Homework_2.UserActions;

public class ADOnetAction : IUserAction
{
    private const string _connectionString = @"Server=DESKTOP-23099KB\sqlexpress;Database=ADOnetDB;Trusted_Connection=True;Encrypt=False;";
    
    private const string _createProductSQL = "INSERT INTO Products VALUES ('{0}', '{1}', '{2}')"; 
    private const string _createCustomerSQL = "INSERT INTO Customers VALUES ('{0}', '{1}', '{2}')";
    private const string _createOrderSQL = "INSERT INTO Orders VALUES ('{0}', '{1}', '{2}')";
    private const string _createOrdersProductsSQL = "INSERT INTO OrdersProducts VALUES ('{0}', '{1}', '{2}')";

    private const string _readAllProductsSQL = "SELECT * FROM Products"; 
    private const string _readAllCustomersSQL = "SELECT * FROM Customers";
    private const string _readAllOrdersSQL = @"
                                    SELECT O.Id, C.FirstName, O.OrderDate, P.ProductName, OP.Quantity FROM Orders AS O
                                    JOIN OrdersProducts AS OP ON OP.OrderId = O.Id
                                    JOIN Products AS P ON P.Id = OP.ProductId
                                    JOIN Customers AS C ON O.CustomerId = C.Id";

    private const string _updateProductSQL = "UPDATE Products SET ProductName = '{0}', Price = '{1}' WHERE Id = '{2}'";
    private const string _updateCustomerSQL = "UPDATE Customer SET FirstName = '{0}', LastName = '{1}' WHERE Id = '{2}'";

    private const string _deleteProductSQL = "DELETE Products WHERE Id = '{0}'";
    private const string _deleteCustomerSQL = "DELETE Customers WHERE Id = '{0}'";
    private const string _deleteOrderSQL = "DELETE Orders WHERE Id = '{0}'";

    private Dictionary<Constants.CRUD, Func<Task>> _crudOperationToImplementation;
    private Dictionary<Constants.DBEntities, Func<Task>> _createDbEntitiesImplementation;
    private Dictionary<Constants.DBEntities, Func<Task>> _readDbEntitiesImplementation;
    private Dictionary<Constants.DBEntities, Func<Task>> _updateDbEntitiesImplementation;
    private Dictionary<Constants.DBEntities, Func<Task>> _deleteDbEntitiesImplementation;

    public ADOnetAction()
    {
        _crudOperationToImplementation = new()
        {
            { Constants.CRUD.Create, Create },
            { Constants.CRUD.Read, Read },
            { Constants.CRUD.Update, Update },
            { Constants.CRUD.Delete, Delete }
        };

        _createDbEntitiesImplementation = new()
        {
            { Constants.DBEntities.Products, CreateProduct },
            { Constants.DBEntities.Customers, CreateCustomer },
            { Constants.DBEntities.Orders, CreateOrder },
        };

        _readDbEntitiesImplementation = new()
        {
            { Constants.DBEntities.Products, ReadAllProducts },
            { Constants.DBEntities.Customers, ReadAllCustomers },
            { Constants.DBEntities.Orders, ReadAllOrders }
        };

        _updateDbEntitiesImplementation = new()
        {
            { Constants.DBEntities.Products, UpdateProduct },
            { Constants.DBEntities.Customers, UpdateCustomer },
        };

        _deleteDbEntitiesImplementation = new()
        {
            { Constants.DBEntities.Products, DeleteProduct },
            { Constants.DBEntities.Customers, DeleteCustomer },
            { Constants.DBEntities.Orders, DeleteOrder },
        };
    }
    
    public async Task MakeAction()
    {
        while (true)
        {
            Console.WriteLine($"{(int)Constants.CRUD.Create}. {Constants.CRUD.Create.ToString()}");
            Console.WriteLine($"{(int)Constants.CRUD.Read}. {Constants.CRUD.Read.ToString()}");
            Console.WriteLine($"{(int)Constants.CRUD.Update}. {Constants.CRUD.Update.ToString()}");
            Console.WriteLine($"{(int)Constants.CRUD.Delete}. {Constants.CRUD.Delete.ToString()}");

            Console.Write("Выберите действие: ");

            if (int.TryParse(Console.ReadLine(), out int selectedOption) &&
                _crudOperationToImplementation.TryGetValue((Constants.CRUD) selectedOption, out var selectedCRUD))
            {
                await selectedCRUD();
                
                while (true)
                {
                    Console.WriteLine("Желаете повторить взаимодействие с ADONet? y/n");
                        
                    ConsoleKey inputKey = Console.ReadKey().Key;
                        
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
                    Console.WriteLine("Введены некорректные данные, попробовать ещё раз? y/n");
                        
                    ConsoleKey inputKey = Console.ReadKey().Key;
                        
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
    
    private async Task CRUDTemplate(Dictionary<Constants.DBEntities, Func<Task>> CRUDImplementation)
    {
        while (true)
        {
            Console.WriteLine($"{(int)Constants.DBEntities.Products}. {Constants.DBEntities.Products.ToString()}");
            Console.WriteLine($"{(int)Constants.DBEntities.Customers}. {Constants.DBEntities.Customers.ToString()}");
            Console.WriteLine($"{(int)Constants.DBEntities.Orders}. {Constants.DBEntities.Orders.ToString()}");

            Console.Write("Выберите действие: ");

            if (int.TryParse(Console.ReadLine(), out int selectedOption) &&
                CRUDImplementation.TryGetValue((Constants.DBEntities)selectedOption, out var selectedQuery))
            {
                await selectedQuery();
                
                while (true)
                {
                    Console.WriteLine("Желаете повторить запрос заново? y/n");
                        
                    ConsoleKey inputKey = Console.ReadKey().Key;
                        
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
                    Console.WriteLine("Введены некорректные данные, попробовать ещё раз? y/n");
                        
                    ConsoleKey inputKey = Console.ReadKey().Key;
                        
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

    private async Task Create()
    {
        await CRUDTemplate(_createDbEntitiesImplementation);
    }

    private async Task CreateProduct()
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

        await ExecuteNonQueryAsync(string.Format(_createProductSQL, Guid.NewGuid(), productName, productPrice));
        
        Console.WriteLine($"Продукт {productName} успешно создан");
    }

    private async Task CreateCustomer()
    {
        Console.Write("Введите имя покупателя: ");
        string customerFirstName = Console.ReadLine();
        
        Console.Write("Введите фамилию покупателя: ");
        string customerLastName = Console.ReadLine();

        await ExecuteNonQueryAsync(string.Format(_createCustomerSQL, Guid.NewGuid(), customerFirstName, customerLastName));
        
        Console.WriteLine($"Пользователь {customerFirstName} успешно создан");
    }

    private async Task CreateOrder()
    {
        var customersIds = await ReadAllCustomers();
        DateTime date = DateTime.Today;

        Console.Write("Выберите покупателя: ");

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int selectedCustomer) && 
                selectedCustomer >= 0 && selectedCustomer < customersIds.Count)
            {
                var selectedCustomerId = customersIds[selectedCustomer];
                
                Guid orderId = Guid.NewGuid();
                bool isOrderCreated = false;

                while (true)
                {
                    var productsIds = await ReadAllProducts();
            
                    Console.Write("Выберите продукт: ");
            
                    if (int.TryParse(Console.ReadLine(), out int selectedProduct) &&
                        selectedProduct >= 0 && selectedProduct < productsIds.Count)
                    {
                        var selectedProductId = productsIds[selectedProduct];
                        
                        Console.Write("Введите количество товара: ");

                        if (int.TryParse(Console.ReadLine(), out int productQuantity) && productQuantity > 0)
                        {
                            if (!isOrderCreated)
                            {
                                await ExecuteNonQueryAsync(string.Format(_createOrderSQL, orderId, selectedCustomerId, date));
                                
                                Console.WriteLine($"Заказ для пользователя {selectedCustomerId} успешно создан");

                                isOrderCreated = true;
                            }

                            await ExecuteNonQueryAsync(string.Format(_createOrdersProductsSQL, orderId,
                                selectedProductId, productQuantity));
                            
                            Console.WriteLine($"Продукт {selectedProductId} добавлен в заказ");
                            Console.WriteLine("Желаете добавить ещё продуктов в заказ? y/n");
                            
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
                        else
                        {
                            Console.WriteLine("Введены некорректные денные, попробуйте ещё раз");
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

    private async Task Read()
    {
        await CRUDTemplate(_readDbEntitiesImplementation);
    }

    private async Task<IList<Guid>> ReadAllProducts()
    {
        var productsIds = new List<Guid>();
        
        await using var sqlConnection = new SqlConnection(_connectionString);
        var sqlCommand = new SqlCommand(_readAllProductsSQL, sqlConnection);
        
        sqlConnection.Open();

        var sqlReader = await sqlCommand.ExecuteReaderAsync();
        var counter = 0;
        
        while (await sqlReader.ReadAsync())
        {
            Guid id = (Guid) sqlReader.GetValue(0);
            productsIds.Add(id);
            
            Console.WriteLine(@$"{counter}. Id: {id}; ProductName: {sqlReader.GetValue(1)}; Price: {sqlReader.GetValue(2)}"
            );

            counter++;
        }

        return productsIds;
    }

    private async Task<IList<Guid>> ReadAllCustomers()
    {
        var customersIds = new List<Guid>();
        
        await using var sqlConnection = new SqlConnection(_connectionString);
        var sqlCommand = new SqlCommand(_readAllCustomersSQL, sqlConnection);
        
        sqlConnection.Open();

        var sqlReader = await sqlCommand.ExecuteReaderAsync();
        var counter = 0;
        
        while (await sqlReader.ReadAsync())
        {
            Guid id = (Guid)sqlReader.GetValue(0);
            
            Console.WriteLine($"{counter}. Id: {id}; FirstName: {sqlReader.GetValue(1)}; LastName: {sqlReader.GetValue(2)}"
            );

            customersIds.Add(id);
            
            counter++;
        }

        return customersIds;
    }

    private async Task<IList<Guid>> ReadAllOrders()
    {
        var ordersIds = new List<Guid>();
        
        await using var sqlConnection = new SqlConnection(_connectionString);
        var sqlCommand = new SqlCommand(_readAllOrdersSQL, sqlConnection);
        
        sqlConnection.Open();

        var sqlReader = await sqlCommand.ExecuteReaderAsync();

        Guid orderId = Guid.Empty;

        int counter = 0;
        
        while (await sqlReader.ReadAsync())
        {
            Guid orderIdFromDb = (Guid)sqlReader.GetValue(0);

            if (orderId != orderIdFromDb)
            {
                orderId = orderIdFromDb;

                ordersIds.Add(orderId);
                Console.WriteLine($"{counter}. Пользователь {sqlReader.GetValue(1)} в заказе {orderId} от {sqlReader.GetValue(2)} заказывал: ");

                counter++;
            }
            
            Console.WriteLine($" --- {sqlReader.GetValue(3)} в количестве {sqlReader.GetValue(4)} единиц товара");
        }

        return ordersIds;
    }

    private async Task Update()
    {
        await CRUDTemplate(_updateDbEntitiesImplementation);
    }

    private async Task UpdateProduct()
    {
        var productsIds = await ReadAllProducts();
        
        while (true)
        {
            Console.Write("Выберите продукт для обновления: ");
            
            if (int.TryParse(Console.ReadLine(), out int selectedProduct) && 
                selectedProduct >= 0 && selectedProduct < productsIds.Count)
            {
                var selectedProductId = productsIds[selectedProduct];
                
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
                
                await ExecuteNonQueryAsync(string.Format(_updateProductSQL, productName, productPrice, selectedProductId));
        
                Console.WriteLine($"Продукт c Id {selectedProductId} успешно изменён");
                
                break;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }

    private async Task UpdateCustomer()
    {
        var customersIds = await ReadAllCustomers();
        
        while (true)
        {
            Console.Write("Выберите пользователя для обновления: ");
            
            if (int.TryParse(Console.ReadLine(), out int selectedCustomer) && 
                selectedCustomer >= 0 && selectedCustomer < customersIds.Count)
            {
                var selectedCustomerId = customersIds[selectedCustomer];
                
                Console.Write("Введите новое имя покупателя: ");
                string customerFirstName = Console.ReadLine();
        
                Console.Write("Введите новую фамилию покупателя: ");
                string customerLastName = Console.ReadLine();
                
                await ExecuteNonQueryAsync(string.Format(_updateCustomerSQL, customerFirstName, customerLastName, selectedCustomerId));
        
                Console.WriteLine($"Пользовательские данные успешно изменёны");
                
                break;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }
    
    private async Task Delete()
    {
        await CRUDTemplate(_deleteDbEntitiesImplementation);
    }

    private async Task DeleteProduct()
    {
        var productsIds = await ReadAllProducts();

        while (true)
        {
            Console.Write("Выберите продукт для удаления: ");
            
            if (int.TryParse(Console.ReadLine(), out int selectedProduct) && 
                selectedProduct >= 0 && selectedProduct < productsIds.Count)
            {
                await ExecuteNonQueryAsync(string.Format(_deleteProductSQL, productsIds[selectedProduct]));
        
                Console.WriteLine($"Продукт c Id {selectedProduct} успешно удалён");
                
                break;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }

    private async Task DeleteCustomer()
    {
        var customersIds = await ReadAllCustomers();

        while (true)
        {
            Console.Write("Выберите пользователя для удаления: ");
            
            if (int.TryParse(Console.ReadLine(), out int selectedCustomerId) && 
                selectedCustomerId >= 0 && selectedCustomerId < customersIds.Count)
            {
                await ExecuteNonQueryAsync(string.Format(_deleteCustomerSQL, customersIds[selectedCustomerId]));
        
                Console.WriteLine($"Пользователь c Id {selectedCustomerId} успешно удалён");
                
                break;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }

    private async Task DeleteOrder()
    {
        var ordersIds = await ReadAllOrders();
        
        while (true)
        {
            Console.Write("Выберите заказ для удаления: ");
            
            if (int.TryParse(Console.ReadLine(), out int selectedOrder) && 
                selectedOrder >= 0 && selectedOrder < ordersIds.Count)
            {
                await ExecuteNonQueryAsync(string.Format(_deleteOrderSQL, ordersIds[selectedOrder]));
        
                Console.WriteLine($"Заказ c Id {selectedOrder} успешно удалён");
                
                break;
            }

            Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
        }
    }

    private async Task ExecuteNonQueryAsync(string query)
    {
        await using var sqlConnection = new SqlConnection(_connectionString);
        var sqlCommand = new SqlCommand(query, sqlConnection);

        sqlConnection.Open();
        await sqlCommand.ExecuteNonQueryAsync();
    }
}
