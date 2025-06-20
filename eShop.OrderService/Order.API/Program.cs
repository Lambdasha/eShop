using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Order.Application.Services;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) EF + DI
builder.Services.AddDbContext<OrderDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbConnection")));
builder.Services.AddScoped<IOrderRepository,      OrderRepository>();
builder.Services.AddScoped<IOrderService,         OrderService>();
builder.Services.AddScoped<ICustomerRepository,      CustomerRepository>();
builder.Services.AddScoped<ICustomerService,         CustomerService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartService,  ShoppingCartService>();

// 2) Controllers
builder.Services.AddControllers();

// 3) Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title   = "Order Microservice API",
        Version = "v1"
    });
});

var app = builder.Build();

// 4) Enable Swagger **before** MapControllers
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // this JSON endpoint must match your doc name ("v1")
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API v1");
        // optional: serve the UI at `/` instead of `/swagger`
        // c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();