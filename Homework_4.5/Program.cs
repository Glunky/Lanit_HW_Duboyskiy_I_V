using Core.Requests;
using Core.Requests.Customers;
using Core.Requests.Orders;
using Core.Requests.Products;
using Core.Responses.Customers;
using Core.Responses.Orders;
using Core.Responses.Products;
using Homework_4._5.Commands.Customers;
using Homework_4._5.Commands.Customers.Interfaces;
using Homework_4._5.Commands.Orders;
using Homework_4._5.Commands.Orders.Interfaces;
using Homework_4._5.Commands.Products;
using Homework_4._5.Commands.Products.Interfaces;
using Homework_4._5.Controllers;
using Homework_4._5.Mappers;
using Homework_4._5.Requests;
using Homework_4._5.Validation;
using Homework_4._5.Validation.Customers;
using Homework_4._5.Validation.Orders;
using Homework_4._5.Validation.Products;
using Homework_4.DbModels;
using Homework_4.Provider;
using Homework_4.Repositories;
using Homework_4.Repositories.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PurchaseDbContext>(options =>
    options.UseSqlServer(PurchaseDbContext.ConnectionString));
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IRepository<DbCustomer, CustomerInfo>), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(IRepository<DbProduct, ProductInfo>), typeof(ProductRepository));
builder.Services.AddScoped(typeof(IRepository<DbOrder, OrderInfo>), typeof(OrderRepository));

builder.Services.AddTransient<ICreateCustomerCommand, CreateCustomerCommand>();
builder.Services.AddTransient<IGetCustomerCommand, GetCustomerCommand>();
builder.Services.AddTransient<IGetAllCustomersCommand, GetAllCustomersCommand>();
builder.Services.AddTransient<IUpdateCustomerCommand, UpdateCustomerCommand>();
builder.Services.AddTransient<IDeleteCustomerCommand, DeleteCustomerCommand>();

builder.Services.AddTransient<ICreateProductCommand, CreateProductCommand>();
builder.Services.AddTransient<IGetProductCommand, GetProductCommand>();
builder.Services.AddTransient<IGetAllProductsCommand, GetAllProductsCommand>();
builder.Services.AddTransient<IUpdateProductCommand, UpdateProductCommand>();
builder.Services.AddTransient<IDeleteProductCommand, DeleteProductCommand>();

builder.Services.AddTransient<ICreateOrderCommand, CreateOrderCommand>();
builder.Services.AddTransient<IGetOrderCommand, GetOrderCommand>();
builder.Services.AddTransient<IGetAllOrdersCommand, GetAllOrdersCommand>();
builder.Services.AddTransient<IUpdateOrderCommand, UpdateOrderCommand>();
builder.Services.AddTransient<IDeleteOrderCommand, DeleteOrderCommand>();

builder.Services.AddTransient<IMapper<CreateCustomerRequest, DbCustomer>, CreateCustomerRequestToDbCustomerMapper>();
builder.Services.AddTransient<IMapper<DbCustomer, GetCustomerResponse>, DbCustomerToGetCustomerResponseMapper>();

builder.Services.AddTransient<IMapper<CreateProductRequest, DbProduct>, CreateProductRequestToDbProductMapper>();
builder.Services.AddTransient<IMapper<DbProduct, GetProductResponse>, DbProductToGetProductResponseMapper>();

builder.Services.AddTransient<IMapper<DbOrder, GetOrderResponse>, DbOrderToGetOrderResponseMapper>();
builder.Services.AddTransient<IMapper<CreateOrderRequest, DbOrder>, CreateOrderRequestToDbOrderMapper>();

builder.Services.AddTransient<IValidator<CreateCustomerRequest>, CreateCustomerValidator>();
builder.Services.AddTransient<IValidator<UpdateCustomerRequest>, UpdateCustomerValidator>();

builder.Services.AddTransient<IValidator<CreateProductRequest>, CreateProductValidator>();
builder.Services.AddTransient<IValidator<UpdateProductRequest>, UpdateProductValidator>();


builder.Services.AddTransient<IValidator<CreateOrderRequest>, CreateOrderValidator>();
builder.Services.AddTransient<IValidator<UpdateOrderRequest>, UpdateOrderValidator>();

try
{
    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumers(typeof(Program).Assembly);

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("localhost");
            cfg.ConfigureEndpoints(context);
        });
    });
}
catch (Exception)
{

    throw new Exception("Failed to connect to rabbitmq");
}

var app = builder.Build();

app.MapControllers();

app.Run();
