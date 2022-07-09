using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Data.Repos;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson().Services
    .AddDbContext<DbContext, ShopContext>()
    .AddHttpClient()

// Add repositories to the container.
    .AddScoped<OrderRepo>()
    .AddScoped<CustomerRepo>()
    .AddScoped<EmployeeRepo>()
    .AddScoped<CoffeeRepo>()

// Add services to the container.
    .AddScoped<OrderService>()
    .AddScoped<CustomerService>()
    .AddScoped<EmployeeService>()
    .AddScoped<CoffeeService>()
    .AddScoped<MorbulateService>()

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
