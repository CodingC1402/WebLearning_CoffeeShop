using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.Data.Validator;
using WebAPI.Services;

const string MyCORSPolicyName = "_myCORSPolicy";

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services
    .AddControllers()
    .AddNewtonsoftJson().Services
    .AddDbContext<DbContext, ShopContext>()
    .AddHttpClient()

// Add services to the container.
    .AddScoped<JwtService>()
    .AddScoped<PopulateService>()

    .AddScoped<IValidator<Employee>, EmployeeValidator>()
    .AddScoped<IValidator<Customer>, CustomerValidator>()
    .AddScoped<IValidator<Shop>, ShopValidator>()
    .AddScoped<IValidator<Coffee>, CoffeeValidator>()
    .AddScoped<IValidator<Order>, OrderValidator>()

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCors(options => {
        options.AddPolicy(MyCORSPolicyName, policy => {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    })

    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    
    // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
    
            ValidAudience = config["JWT:ValidAudience"],
            ValidIssuer = config["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]))
        };
    });
        
    var app = builder.Build();
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseCors(MyCORSPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();