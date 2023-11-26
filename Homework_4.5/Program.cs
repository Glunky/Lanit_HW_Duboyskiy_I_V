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
using Homework_4.DbModels;
using Homework_4.Provider;
using Homework_4.Repositories;
using Homework_4.Repositories.Interfaces;
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

builder.Services.AddTransient<IMapper<CustomerInfo, DbCustomer>, CustomerInfoToDbCustomerMapper>();
builder.Services.AddTransient<IMapper<DbCustomer, CustomerInfo>, DbCustomerToCustomerInfoMapper>();

builder.Services.AddTransient<IMapper<ProductInfo, DbProduct>, ProductInfoToDbProductMapper>();
builder.Services.AddTransient<IMapper<DbProduct, ProductInfo>, DbProductToProductInfoMapper>();

builder.Services.AddTransient<IMapper<DbOrder, OrderInfo>, DbOrderToOrderInfoMapper>();
builder.Services.AddTransient<IMapper<OrderInfo, DbOrder>, OrderInfoToDbOrderMapper>();

builder.Services.AddTransient<IValidator<CustomerInfo>, CustomerValidator>();
builder.Services.AddTransient<IValidator<ProductInfo>, ProductValidator>();
builder.Services.AddTransient<IValidator<OrderInfo>, OrderValidator>();

var app = builder.Build();

app.MapControllers();

app.Run();