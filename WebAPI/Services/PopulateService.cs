using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.DTOs.RandomInfoAPI;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Services;

public class PopulateService : Service
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ShopContext _context;

    public PopulateService(ShopContext context, IHttpClientFactory httpClientFactory)
        => (_context, _httpClientFactory) = (context, httpClientFactory);

    public async Task DepopulateCustomer() {
        await _context.Customers.RemoveAll();
        await _context.SaveChangesAsync();
    }
    public async Task PopulateCustomer(int number)
    {
        var infos = await GenerateRandomInfosRange<PeopleDto>(
            "https://api.namefake.com/english-united-states/", 
            s => $"{s}{(Random.Shared.Next() % 2 == 0 ? "female" : "male")}/", 
            number);

        foreach (var info in infos)
        {
            var newCustomer = new Customer
            {
                FullName = info.FullName,
                Email = info.Email,
                PhoneNumber = info.Phone,
                DOB = info.DOB,
                Gender = info.Gender == "female" ? Person.GenderType.Female : Person.GenderType.Male,
                Point = ((uint)Random.Shared.Next(0, 3000))
            };
            newCustomer.RegisterSince = newCustomer.RegisterSince.AddDays(Random.Shared.Next(-2000, 0));

            _context.Customers.Add(newCustomer);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DepopulateCoffee()
    {
        await _context.Coffees.RemoveAll();
        await _context.SaveChangesAsync();
    }
    public async Task PopulateCoffee(int number)
    {
        var infos = await GenerateRandomInfosRange<CoffeeDto>(
            "https://random-data-api.com/api/coffee/random_coffee", 
            s => s, 
            number);

        foreach (var info in infos)
        {
            var newCoffee = new Coffee
            {
                Name = info.Name,
                Origin = info.Origin,
                Notes = info.Notes,
                Variety = info.Variety,
                Price = Random.Shared.Next(20, 250) / 23M
            };

            _context.Coffees.Add(newCoffee);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DepopulateOrder()
    {
        await _context.Orders.RemoveAll();
        await _context.SaveChangesAsync();
    }
    public async Task PopulateOrder(int number)
    {
        for (int i = 0; i < number; i++) {
            var newOrder = new Order();
            var count = Random.Shared.Next(1, 5);

            newOrder.Customer = (await _context.Customers.GetRandomEntity());
            newOrder.Employee = (await _context.Employees.GetRandomEntity());
            newOrder.Details = new List<OrderDetail>(count);
            for (int j = 0; j < count; j++) {
                var coffee = await _context.Coffees.GetRandomEntity();
                var existDetail = newOrder.Details.FirstOrDefault(o => o.Coffee == coffee);
                if (existDetail != null) {
                    existDetail.Count += Random.Shared.Next(1, 2);
                } else {
                    newOrder.Details.Add(new OrderDetail {
                        Coffee = coffee,
                        Order = newOrder,
                        Count = Random.Shared.Next(1, 4)
                    });
                }
            }

            _context.Orders.Add(newOrder);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DepopulateEmployee()
    {
        await _context.Employees.RemoveAll();
        await _context.SaveChangesAsync();
    }
    public async Task PopulateEmployees(int number)
    {
        var infos = await GenerateRandomInfosRange<PeopleDto>(
            "https://api.namefake.com/english-united-states/", 
            s => $"{s}{(Random.Shared.Next() % 2 == 0 ? "female" : "male")}/", 
            number);

        foreach (var info in infos)
        {
            var newEmployee = new Employee
            {
                FullName = info.FullName,
                Email = info.Email,
                PhoneNumber = info.Phone,
                DOB = info.DOB,
                Gender = info.Gender == "female" ? Person.GenderType.Female : Person.GenderType.Male,
            };
            newEmployee.StartDate = newEmployee.StartDate.AddDays(Random.Shared.Next(-2000, 0));

            _context.Employees.Add(newEmployee);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DepopulateShop() {
        await _context.Shop.RemoveAll();
        await _context.SaveChangesAsync();
    }
    public async Task PopulateShop(int number) {
        for (int i = 0; i < number; i++) {
            var shop = new Shop {
                Address = $"{Random.Shared.Next(1, 123)}, {Random.Shared.Next(1, 12)} street, district {Random.Shared.Next(1, 12)}, HCM city",
                EstablishedSince = DateTime.Now.AddDays(-Random.Shared.Next(1, 3000)),
                Phone = $"(+{Random.Shared.Next(1, 99)}){Random.Shared.Next(10, 99)}-{Random.Shared.Next(100, 999)}-{Random.Shared.Next(1000, 9999)}"
            };

            _context.Add(shop);
        }

        await _context.SaveChangesAsync();
    }


    public async Task<T> GenerateRandomInfo<T> (
        string url, 
        Func<string, string> urlModifier, 
        int retry = 0)
    {
        const int retryDelay = 100;

        if (retry > 5)
        {
            throw new Exception("Failed to generate random info after 5 attempts");
        }

        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(urlModifier(url));

            if (!response.IsSuccessStatusCode)
            {
                await Task.Delay(retryDelay);
                return await GenerateRandomInfo<T>(url, urlModifier, ++retry);
            }
            else
            {
                var info = await response.Content.ReadFromJsonAsync<T>();

                if (info == null)
                {
                    return await GenerateRandomInfo<T>(url, urlModifier, ++retry);
                }
                else
                {
                    Validator.ValidateObject(info, new ValidationContext(info), true);
                    return info;
                }
            }
        }
        catch (Exception)
        {
            await Task.Delay(retryDelay);
            return await GenerateRandomInfo<T>(url, urlModifier, ++retry);
        }
    }
    public async Task<T[]> GenerateRandomInfosRange<T>(
        string url,
        Func<string, string> modifier,
        int number) where T : class 
    {
        T[] infos = new T[number];
        Task[] tasks = new Task[number];

        for (int i = 0; i < number; i++)
        {
            int index = i;
            tasks[index] = Task.Run(async () =>
            {
                infos[index] = await GenerateRandomInfo<T>(url, modifier, 0);
            });
        }
        await Task.WhenAll(tasks);
        return infos;
    }
}