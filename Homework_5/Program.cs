using Core.Requests.Customers;
using Core.Requests.Orders;
using Core.Requests.Products;
using Core.Responses.Customers;
using Core.Responses.Orders;
using Core.Responses.Products;
using Homework_5.Publishers;
using Homework_5.Publishers.Customers;
using Homework_5.Publishers.Orders;
using Homework_5.Publishers.Products;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IMessagePublisher<CreateCustomerRequest, CreateCustomerResponse>, CreateCustomerMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<GetCustomerRequest, GetCustomerResponse>, GetCustomerMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<GetAllCustomersRequest, GetAllCustomersResponse>, GetAllCustomersPublisher>();
builder.Services.AddScoped<IMessagePublisher<UpdateCustomerRequest, UpdateCustomerResponse>, UpdateCustomerMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<DeleteCustomerRequest, DeleteCustomerResponse>, DeleteCustomerMessagePublisher>();

builder.Services.AddScoped<IMessagePublisher<CreateProductRequest, CreateProductResponse>, CreateProductMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<GetProductRequest, GetProductResponse>, GetProductMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<GetAllProductsRequest, GetAllProductsResponse>, GetAllProductsMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<UpdateProductRequest, UpdateProductResponse>, UpdateProductMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<DeleteProductRequest, DeleteProductResponse>, DeleteProductMessagePublisher>();

builder.Services.AddScoped<IMessagePublisher<CreateOrderRequest, CreateOrderResponse>, CreateOrderMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<GetOrderRequest, GetOrderResponse>, GetOrderMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<GetAllOrdersRequest, GetAllOrdersResponse>, GetAllOrdersMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<UpdateOrderRequest, UpdateOrderResponse>, UpdateOrderMessagePublisher>();
builder.Services.AddScoped<IMessagePublisher<DeleteOrderRequest, DeleteOrderResponse>, DeleteOrderMessagePublisher>();

try
{
    builder.Services.AddMassTransit(x =>
    {
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
