using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductService.Application.Services;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repositories;
using ProductService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ProductDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ProductDbConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService,    ProductService.Infrastructure.Services.ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService,    CategoryService>();
builder.Services.AddScoped<ICategoryVariationRepository, CategoryVariationRepository>();
builder.Services.AddScoped<ICategoryVariationService,    CategoryVariationService>();


// Swagger/OpenAPI
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