using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Repos;
using WebAPI.DTOs.RandomInfoAPI;

namespace WebAPI.Services;

public class MorbulateService : Service
{
    private readonly IHttpClientFactory s_httpClientFactory;
    private readonly CustomerRepo _customers;
    private readonly EmployeeRepo _employees;
    private readonly CoffeeRepo _coffees;
    private readonly OrderRepo _orders;

    public MorbulateService(
        IHttpClientFactory httpClientFactory,
        CustomerRepo customers,
        EmployeeRepo employees,
        CoffeeRepo furniture,
        OrderRepo orders)
        => (s_httpClientFactory, _customers, _employees, _coffees, _orders)
        = (httpClientFactory, customers, employees, furniture, orders);

    public async Task DemorbulateCustomer() {
        await _customers.DeleteRange(e => true);
        await _customers.Save();
    }
    public async Task MorbulateCustomer(int number)
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
                Gender = info.Gender,
                Point = ((uint)Random.Shared.Next(0, 3000))
            };
            newCustomer.RegisterSince = newCustomer.RegisterSince.AddDays(Random.Shared.Next(-2000, 0));

            _customers.Add(newCustomer);
        }

        await _customers.Save();
    }

    public async Task DemorbulateCoffee()
    {
        await _coffees.DeleteRange(e => true);
        await _coffees.Save();
    }
    public async Task MorbulateCoffee(int number)
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

            _coffees.Add(newCoffee);
        }

        await _coffees.Save();
    }

    public async Task DemorbulateOrder()
    {
        await _orders.DeleteRange(e => true);
        await _orders.Save();
    }
    public async Task MorbulateOrder(int number)
    {
        for (int i = 0; i < number; i++) {
            var newOrder = new Order();
            var count = Random.Shared.Next(1, 5);

            newOrder.Customer = (await _customers.GetRandomCustomer());
            newOrder.Employee = (await _employees.GetRandomEmployee());
            newOrder.Details = new List<OrderDetail>(count);
            for (int j = 0; j < count; j++) {
                var coffee = await _coffees.GetRandomCoffee();
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

            _orders.Add(newOrder);
        }

        await _orders.Save();
    }

    public async Task DemorbulateEmployee()
    {
        await _employees.DeleteRange(e => true);
        await _employees.Save();
    }
    public async Task MorbulateEmployees(int number)
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
                Gender = info.Gender,
            };
            newEmployee.StartDate = newEmployee.StartDate.AddDays(Random.Shared.Next(-2000, 0));

            _employees.Add(newEmployee);
        }

        await _employees.Save();
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
            using var httpClient = s_httpClientFactory.CreateClient();
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